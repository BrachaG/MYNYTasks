using Entities;
using Microsoft.Extensions.Logging;
using NLog;
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
        public async Task<List<Target>> GetTargetsByUserId(int UserId, int PermissionLevelId)
        {
            _logger.LogDebug("GetTargetsByUserId", UserId, PermissionLevelId);
            List<SqlParameter> parameters = new List<SqlParameter> {
            { new SqlParameter("id",UserId )},
            { new SqlParameter("PermissionLevelId", PermissionLevelId)}
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
        public async Task AddTarget(string Comment, int TargetId, int[] PersonId, DateTime? TargetDate)
        {
            DataTable personIds = new DataTable();
            personIds.Columns.Add("Id", typeof(int));

            // Populate the DataTable with the list of PersonId values
            foreach (int id in PersonId)
            {
                personIds.Rows.Add(id);
            }

            List<SqlParameter> parameters = new List<SqlParameter> {
            new SqlParameter("Comment", Comment),
            new SqlParameter("TargetId", TargetId),
            new SqlParameter
            {
                ParameterName = "Ids",
                SqlDbType = SqlDbType.Structured,
                TypeName = "dbo.PersonIds",
                Value = personIds
            },
            new SqlParameter("TargetDate", TargetDate)
            };  

            try
            {
                await _SqlDataAccess.ExecuteDatatableSP("Insert_Target", parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to insert target", ex);
            }
        }
    }
}
