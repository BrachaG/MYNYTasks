using Entities;
using Microsoft.AspNetCore.Mvc;
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
    public class SettingsService : ISettingsService
    {
        ISqlDataAccess _SqlDataAccess;
        ILogger<SettingsService> _logger;
        IObjectGenerator<TaskType> _taskTypeObjectGenerator;
        IObjectGenerator<TargetType> _targetTypeObjectGenerator;
        IObjectGenerator<TargetStatus> _targetStatusObjectGenerator;
        public SettingsService(ISqlDataAccess SqlDataAccess, ILogger<SettingsService> logger, IObjectGenerator<TaskType> taskTypeObjectGenerator, IObjectGenerator<TargetType> targetTypeObjectGenerator, IObjectGenerator<TargetStatus> targetStatusObjectGenerator)
        {
            _SqlDataAccess = SqlDataAccess;
            _logger = logger;
            _taskTypeObjectGenerator = taskTypeObjectGenerator;
            _targetTypeObjectGenerator = targetTypeObjectGenerator;
            _targetStatusObjectGenerator = targetStatusObjectGenerator;
        }

        public async Task<IActionResult> AddStatus(string name, string permissionLevel, string userId)
        {
            _logger.LogDebug("in Add Target Status");
            SqlParameter[] sp = new SqlParameter[]
                 {
                    new SqlParameter("iUserId",userId),
                    new SqlParameter("iPermissionId",permissionLevel ),
                    new SqlParameter("nvStatusName", name)
                 };
            try
            {
                await _SqlDataAccess.ExecuteScalarSP("su_AddStatus_INS", sp);
                return new StatusCodeResult(200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "in SettingsService, add new Target Status, failed when trying to approach the database");
                return new StatusCodeResult(400);
            }
        }

        public async Task<IActionResult> AddTargetType(string name, string permissionLevel, string userId)
        {
            _logger.LogDebug("in Add Target Type");
            SqlParameter[] sp = new SqlParameter[]
                 {
                    new SqlParameter("nvTypeName", name),
                    new SqlParameter("iUserId",userId),
                    new SqlParameter("iPermissionLevel",permissionLevel )
                 };
            try
            {
                await _SqlDataAccess.ExecuteScalarSP("su_AddTargetType_INS", sp);
                return new StatusCodeResult(200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "in SettingsService, add new Target Type, failed when trying to approach the database");
                return new StatusCodeResult(400);
            }
        }

        public async Task<IActionResult> AddTaskType(string name, string permissionLevel)
        {
            _logger.LogDebug("in Add Task Type");
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
            catch(Exception ex)
            {
                _logger.LogError(ex, "in SettingsService, add new Task Type, failed when trying to approach the database");
                return new StatusCodeResult(400);
            }
        }

        public Task<IActionResult> CreateBranchGroup(string name, string permissionLevel)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<List<TargetStatus>>> GetStatuses()
        {
            _logger.LogDebug("in Get Targets Status");
            try
            {
                DataTable dt = await _SqlDataAccess.ExecuteDatatableSP("su_GetTargetType", null);
                List<TargetStatus> targetStatus = _targetStatusObjectGenerator.GeneratListFromDataTable(dt);
                return targetStatus;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "in SettingsService,Get Target Status, failed when trying to approach the database");
                return new StatusCodeResult(400);
            }
        }

        public async Task<ActionResult<List<TargetType>>> GetTargetTypes()
        {
            _logger.LogDebug("in Get Targets Type");
            try
            {
                DataTable dt=await _SqlDataAccess.ExecuteDatatableSP("su_GetTargetType", null);
                List<TargetType> targetsType= _targetTypeObjectGenerator.GeneratListFromDataTable(dt);
                return targetsType;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "in SettingsService,Get Target Type, failed when trying to approach the database");
                return new StatusCodeResult(400);
            }
           
        }

        public async Task<ActionResult<List<TaskType>>> GetTaskTypes()
        {
            _logger.LogDebug("in Get all Task Types");
            try
            {
                DataTable dt = await _SqlDataAccess.ExecuteDatatableSP("su_GetAllTaskTypes", null);
                List<TaskType> taskTypes = _taskTypeObjectGenerator.GeneratListFromDataTable(dt);
                return taskTypes;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "in SettingsService, get all Task Types, failed when trying to approach the database");
                return new StatusCodeResult(400);
            }
        }
    }
}
