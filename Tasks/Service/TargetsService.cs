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
        IObjectGenerator<Target> _userObjectGenerator;

        public TargetsService(ISqlDataAccess SqlDataAccess, ILogger<TargetsService> logger, IObjectGenerator<Target> userObjectGenerator)
        {
            _SqlDataAccess = SqlDataAccess;
            _logger = logger;
            _userObjectGenerator= userObjectGenerator;
        }
        public async Task<string> GetTargetsByUserId(int UserId, int Status)
        {
            _logger.LogDebug("GetTargetsByUserId", UserId, Status);
            List<SqlParameter> parameters = new List<SqlParameter> {
            { new SqlParameter("id",UserId )},
            { new SqlParameter("status", Status)}
                };
            try
            {

                DataTable targets = await _SqlDataAccess.ExecuteDatatableSP("Get_Targets", parameters);
                if(targets.Rows.Count>0)
                {
                   Target t = _userObjectGenerator.GeneratListFromDataTable(targets);

                }



            }
            catch (Exception ex) {
                _logger.LogError("GetTargetsByUserId ", ex);
            }
            return  "ok";
            }
     
    }
}
