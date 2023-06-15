using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class SettingsService : ISettingsService
    {
        ISqlDataAccess _SqlDataAccess;
        ILogger<SettingsService> _logger;
        public SettingsService(ISqlDataAccess SqlDataAccess, ILogger<SettingsService> logger)
        {
            _SqlDataAccess = SqlDataAccess;
            _logger = logger;

        }
        public async Task<IActionResult> AddStatus(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> AddTargetType(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> AddTaskType(string name, string permissionLevel)
        {
            SqlParameter [] sp = new SqlParameter[]
                 {
                    new SqlParameter("nvTypeName", name),
                    new SqlParameter("iPermissionLevelId",permissionLevel )
                 };
            try
            {
                await _SqlDataAccess.ExecuteScalarSP("su_AddTaskType_INS", sp);
                return new StatusCodeResult(200);
            }
            catch
            {
                return new StatusCodeResult(400);
            }
        }
        public async Task<IActionResult> CreateBranchGroup(string name)
        {
            throw new NotImplementedException();
        }
    }
}
