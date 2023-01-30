using Entities;
using Repository;
using System.Data;
using System.Data.SqlClient;

namespace Service
{
    public class UsersService : IUsersService
    {
        IUsersRepository _UsersRepository;
        ISqlDataAccess _SqlDataAccess;
        public UsersService(IUsersRepository UsersRepository, ISqlDataAccess SqlDataAccess)
        {
            _UsersRepository = UsersRepository;
            _SqlDataAccess =SqlDataAccess;
        }
        public async Task<Object> GetById(string userName, string password)
        {
            SqlParameter[] parameters = {new SqlParameter("nvUserName",userName),
                                             new SqlParameter("nvPassword",password),
                                             new SqlParameter("nvAddress",""),
                                             new SqlParameter("iPort",0)
                                             };
            List<SqlParameter> p = new List<SqlParameter> { 
            { new SqlParameter("nvUserName",userName )},
                                             { new SqlParameter("nvPassword", password)},
                                             { new SqlParameter("nvAddress","")},
            { new SqlParameter("iPort", 0)}
                                         };
            try
            {
                var a = await _SqlDataAccess.ExecuteDatasetSP("ed_sys_User_SLCT", p);
            }
            catch(Exception ex)
            {
                var b = ex.Message;
            }
            return null;
            //return await _SqlDataAccess.ExecuteScalarSP("ed_sys_User_SLCT", parameters);

            ////return await _SqlDataAccess.ExecuteDatatableSP("ed_sys_User_SLCT", p); 


        }
    }
}