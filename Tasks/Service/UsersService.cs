using AutoMapper;
using Entities;
using Repository;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;

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
            //return await _UsersRepository.GetById(userName, password);
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
                    user.lBranches = _codeTableGenerator.GeneratListFromDataRowCollection(ds.Tables[1].Rows);
                return  user;
            }
            else return new User() { iUserId = -1 };
        }
    }
    }