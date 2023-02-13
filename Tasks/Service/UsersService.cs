
using Entities;
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
        public UsersService(ISqlDataAccess SqlDataAccess, IObjectGenerator<User> userObjectGenerator, IObjectGenerator<CodeTable> codeTableGenerator)
        {
            _userObjectGenerator = userObjectGenerator;
            _codeTableGenerator = codeTableGenerator;
            _SqlDataAccess = SqlDataAccess;
        }
        public async Task<User> GetById(string userName, string password)
        {
            SqlParameter[] parameters = {new SqlParameter("nvUserName",userName),
                                             new SqlParameter("nvPassword",password),
                                             new SqlParameter("nvAddress",""),
                                             new SqlParameter("iPort",0)
                                             };
            List<SqlParameter> p = new List<SqlParameter> {
            { new SqlParameter("nvUserName",userName )},
                                             { new SqlParameter("nvPassword", password)}



                };
            DataSet ds = await _SqlDataAccess.ExecuteDatasetSP("PRG_sys_User_SLCT", p);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && int.Parse(ds.Tables[0].Rows[0]["iUserId"].ToString()) > 0)
            {
                User user = _userObjectGenerator.GeneratFromDataRow(ds.Tables[0].Rows[0]);
                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    user.lBranches = _codeTableGenerator.GeneratListFromDataTable(ds.Tables[1]);
                string userToken = GenarateToken(user);
                user.token = userToken;
                user.iUserId = 0;
                return user;
            }
            else return null;
        }

        private static string GenarateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ygrcuy3gcryh@$#^%*&^(_+"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            string jsonString = System.Text.Json.JsonSerializer.Serialize(user);

            var claims = new List<Claim>
    {
            new Claim(JwtRegisteredClaimNames.Sub, jsonString),
        
    };

            var token = new JwtSecurityToken(
                issuer: "nefesh",
                audience: "localhost:4200",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        }
    }