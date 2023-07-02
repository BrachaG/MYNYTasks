using Entities;
using Microsoft.AspNetCore.Mvc;
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
        ISqlDataAccess _sqlDataAccess;
        IObjectGenerator<User> _userObjectGenerator;
        IObjectGenerator<Branch> _branchGenerator;
        IConfiguration _configuration;
        readonly string Issure;
        readonly string Audience;
        ILogger<UsersService> _logger;

        public UsersService(ISqlDataAccess sqlDataAccess, IObjectGenerator<User> userObjectGenerator, IObjectGenerator<Branch> branchGenerator, ILogger<UsersService> logger, IConfiguration configuration)

        {
            _userObjectGenerator = userObjectGenerator;
            _branchGenerator = branchGenerator;
            _sqlDataAccess = sqlDataAccess;
            _configuration = configuration;
            Issure = _configuration["JWTParams:Issure"];
            Audience = _configuration["JWTParams:Audience"];
            _logger = logger;

        }
        public async Task<ActionResult<User>> GetById(string userName, string password)
        {
            _logger.LogDebug("GetById", userName);
            List<SqlParameter> p = new List<SqlParameter> {
            { new SqlParameter("nvUserName",userName )},
                                             { new SqlParameter("nvPassword", password)}
                };
            try
            {
                DataSet ds = await _sqlDataAccess.ExecuteDatasetSP("PRG_su_sys_UserLogin_SLCT", p);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && int.Parse(ds.Tables[0].Rows[0]["iUserId"].ToString()) > 0)
                {
                    User user = _userObjectGenerator.GeneratFromDataRow(ds.Tables[0].Rows[0]);
                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                        user.lBranches = _branchGenerator.GeneratListFromDataTable(ds.Tables[1]);
                    string userToken = GenarateToken(user.iUserId, user.iPermissionLevelId);
                    user.token = userToken;
                    user.iUserId = 0;
                    return new ObjectResult(user) { StatusCode = 200 };
                }
                else return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "in UserService, Login, Get, When trying to approach to Database");
                return new ObjectResult(null) { StatusCode = 500 };
            }

        }
        public string GenarateToken(int userId, int permissionLevelId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ygrcuy3gcryh@$#^%*&^(_+"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            string IdjsonString = userId.ToString();
            string PermissionLevelIdJsonString = permissionLevelId.ToString();
            var claims = new List<Claim>
            {    new Claim(JwtRegisteredClaimNames.NameId, IdjsonString) ,
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