using System.Data;
using System.Data.SqlClient;

namespace Repository
{
    public interface ISqlDataAccess
    {
        public Task<DataSet> ExecuteDatasetSP(string spName, List<SqlParameter> SPParameters);
        public Task<object> ExecuteScalarSP(string spName, params SqlParameter[] commandParameters);
        public Task<DataTable> ExecuteDatatableSP(string spName, List<SqlParameter> SPParameters);
    }
}