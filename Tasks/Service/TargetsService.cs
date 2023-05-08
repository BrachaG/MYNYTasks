using Entities;
using Microsoft.Extensions.Logging;
using Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class TargetsService : ITargetsService
    {
        ISqlDataAccess _SqlDataAccess;
        ILogger<TargetsService> _logger;
        IObjectGenerator<Target> _targetObjectGenerator;

        public TargetsService(ISqlDataAccess SqlDataAccess, ILogger<TargetsService> logger, IObjectGenerator<Target> userObjectGenerator)
        {
            _SqlDataAccess = SqlDataAccess;
            _logger = logger;
            _targetObjectGenerator = userObjectGenerator;
        }
        public async Task<List<Target>> GetTargetsByUserId(int UserId, int Status)
        {
            _logger.LogDebug("GetTargetsByUserId", UserId, Status);
            List<SqlParameter> parameters = new List<SqlParameter> {
            { new SqlParameter("id",UserId )},
            { new SqlParameter("status", Status)}
                };
            try
            {
                DataTable targets = await _SqlDataAccess.ExecuteDatatableSP("Get_Targets", parameters);
                if (targets.Rows.Count > 0)
                {
                    List<Target> t = _targetObjectGenerator.GeneratListFromDataTable(targets);
                    return t;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("GetTargetsByUserId ", ex);
            }
            return null;
        }
        public async Task AddTarget(String Comment, int TargetId, int PersonId)
        {
            List<SqlParameter> parameters = new List<SqlParameter> {
            { new SqlParameter("Comment",Comment )},
            { new SqlParameter("TargetId", TargetId)},
            { new SqlParameter("PersonId", PersonId)}
                };
            try
            {
                await _SqlDataAccess.ExecuteDatatableSP("Insert_Target", parameters);
            }
            catch (Exception ex)
            {

            }

        }
    }
}
