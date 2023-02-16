using Entities;
using Microsoft.Extensions.Logging;
using NLog;
using Repository;
using System.Data;
using System.Data.SqlClient;


namespace Service
{
    public class UsersService : IUsersService
    {
        ISqlDataAccess _SqlDataAccess;
        IObjectGenerator<User> _userObjectGenerator;
        IObjectGenerator<CodeTable> _codeTableGenerator;
        ILogger<UsersService> _logger;
        public UsersService(ISqlDataAccess SqlDataAccess, IObjectGenerator<User> userObjectGenerator, IObjectGenerator<CodeTable> codeTableGenerator, ILogger<UsersService> logger)

        {
            _userObjectGenerator = userObjectGenerator;
            _codeTableGenerator = codeTableGenerator;
            _SqlDataAccess = SqlDataAccess;
            _logger = logger;

        }
        int result;
        public async Task<User> GetById(string userName, string password)
        {
            List<SqlParameter> parameters = new List<SqlParameter> {
            { new SqlParameter("nvUserName",userName )},
                                             { new SqlParameter("nvPassword", password)}
                };
            try
            {
                DataSet ds = await _SqlDataAccess.ExecuteDatasetSP("PRG_sys_User_SLCT", parameters);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && int.TryParse(ds.Tables[0].Rows[0]["iUserId"].ToString(),out result))
            {
                User user = _userObjectGenerator.GeneratFromDataRow(ds.Tables[0].Rows[0]);
                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    user.lBranches = _codeTableGenerator.GeneratListFromDataTable(ds.Tables[1]);
                return user;
            }
            else return new User() { iUserId = -1 };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "in userService, login, get, when trying to approach to database");
                return new User() { iUserId = -1 };
            }
        }
    }
}