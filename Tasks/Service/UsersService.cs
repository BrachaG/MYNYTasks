using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Repository;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service
{
    public class UsersService : IUsersService
    {
        ISqlDataAccess _SqlDataAccess;
        IObjectGenerator<User> _userObjectGenerator;
        IObjectGenerator<CodeTable> _codeTableGenerator;
        IConfiguration _Configuration;
        readonly string Issure;
        readonly string Audience;
        ILogger<UsersService> _logger;

        public UsersService(ISqlDataAccess SqlDataAccess, IObjectGenerator<User> userObjectGenerator, IObjectGenerator<CodeTable> codeTableGenerator, ILogger<UsersService> logger, IConfiguration Configuration)

        {
            _userObjectGenerator = userObjectGenerator;
            _codeTableGenerator = codeTableGenerator;
            _SqlDataAccess = SqlDataAccess;
            _Configuration = Configuration;
            Issure = _Configuration["JWTParams:Issure"];
            Audience = _Configuration["JWTParams:Audience"];
            _logger = logger;

        }
        public async Task<User> GetById(string userName, string password)
        {
            _logger.LogDebug("GetById", userName);
            List<SqlParameter> p = new List<SqlParameter> {
                                     { new SqlParameter("nvUserName",userName )},
                                             { new SqlParameter("nvPassword", password)}
                };
            try
            {
                DataSet ds = await _SqlDataAccess.ExecuteDatasetSP("PRG_su_sys_UserLogin_SLCT", p);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && int.Parse(ds.Tables[0].Rows[0]["iUserId"].ToString()) > 0)
                {
                    User user = _userObjectGenerator.GeneratFromDataRow(ds.Tables[0].Rows[0]);
                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                        user.lBranches = _codeTableGenerator.GeneratListFromDataTable(ds.Tables[1]);
                    string userToken = GenarateToken(user.iUserId, user.iPermissionLevelId);
                    user.token = userToken;
                    user.iUserId = 0;
                    return user;
                }
                else return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "in UserService, Login, Get, When trying to approach to Database");
                return null;
            }

        }
        public string GenarateToken(int UserId, int PermissionLevelId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ygrcuy3gcryh@$#^%*&^(_+"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            string IdjsonString = UserId.ToString();
            string PermissionLevelIdJsonString = PermissionLevelId.ToString();
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, IdjsonString) ,
             new Claim(JwtRegisteredClaimNames.Sub, PermissionLevelIdJsonString)  };
            var token = new JwtSecurityToken(
                issuer: Issure,
                audience: Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}