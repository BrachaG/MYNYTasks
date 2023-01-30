using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ISqlDataAccess
    {

        public Task<DataSet> ExecuteDatasetSP(string spName, List<SqlParameter> SPParameters);
        public Task<object> ExecuteScalarSP(string spName, params SqlParameter[] commandParameters);
        public Task<DataTable> ExecuteDatatableSP(string spName, List<SqlParameter> SPParameters);

       
    }
}