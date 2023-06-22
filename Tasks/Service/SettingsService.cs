using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository;
using System.Data;
using System.Data.SqlClient;

namespace Service
{
    public class SettingsService : ISettingsService
    {
        ISqlDataAccess _SqlDataAccess;
        ILogger<SettingsService> _logger;
        IObjectGenerator<TaskType> _taskTypeObjectGenerator;
        IObjectGenerator<TargetType> _targetTypeObjectGenerator;
        IObjectGenerator<TargetStatus> _targetStatusObjectGenerator;
        IObjectGenerator<BranchGroup> _branchGroupObjectGenerator;
        IObjectGenerator<Branch> _branchObjectGenerator;
        public SettingsService(ISqlDataAccess SqlDataAccess, ILogger<SettingsService> logger, IObjectGenerator<TaskType> taskTypeObjectGenerator, IObjectGenerator<TargetType> targetTypeObjectGenerator, IObjectGenerator<TargetStatus> targetStatusObjectGenerator, IObjectGenerator<BranchGroup> branchGroupObjectGenerator, IObjectGenerator<Branch> branchObjectGenerator)
        {
            _SqlDataAccess = SqlDataAccess;
            _logger = logger;
            _taskTypeObjectGenerator = taskTypeObjectGenerator;
            _targetTypeObjectGenerator = targetTypeObjectGenerator;
            _targetStatusObjectGenerator = targetStatusObjectGenerator;
            _branchGroupObjectGenerator = branchGroupObjectGenerator;
            _branchObjectGenerator = branchObjectGenerator;
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
            SqlParameter[] sp = new SqlParameter[]
                 {
                    new SqlParameter("nvTypeName", name),
                    new SqlParameter("iPermissionLevelId",permissionLevel )
                 };
            try
            {
                await _SqlDataAccess.ExecuteScalarSP("su_AddTaskType_INS", sp);
                return new StatusCodeResult(200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "in SettingsService, add new Task Type, failed when trying to approach the database");
                return new StatusCodeResult(400);
            }
        }

        public async Task<IActionResult> CreateBranchGroup(string name, string permissionLevel, int[] branchesIds)
        {
            DataTable branches = new DataTable();
            branches.Columns.Add("branchesId", typeof(int));
            foreach (int id in branchesIds)
            {
                branches.Rows.Add(id);
            }

            List<SqlParameter> parameters = new List<SqlParameter> {
                    new SqlParameter("iPermissionId",permissionLevel ),
                    new SqlParameter("GroupName", name),
                    new SqlParameter
                    {
                    ParameterName = "Branches",
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "dbo.BranchesIds",
                    Value = branches
                    }
            };
            try
            {
                await _SqlDataAccess.ExecuteDatatableSP("su_CreateBranchesGroup", parameters);
                return new StatusCodeResult(200);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to create group", ex);
                return new StatusCodeResult(400);
            }
        }
        public async Task<ActionResult<List<BranchGroup>>> GetBranchGroup()
        {
            _logger.LogDebug("in Get Branches Group");
            try
            {
                DataSet ds = await _SqlDataAccess.ExecuteDatasetSP("su_GetBranchesGruops_SLCT", null);
                DataTable dtGroups = ds.Tables[0];
                DataTable dtBranches = ds.Tables[1];
                List<BranchGroup> branchGroups = _branchGroupObjectGenerator.GeneratListFromDataTable(dtGroups);
                List<Branch> branches = _branchObjectGenerator.GeneratListFromDataTable(dtBranches);
                foreach (Branch branch in branches)
                {
                    BranchGroup? branchGroup = branchGroups.Find(group => group.iGroupId == branch.iGroupId);
                    if (branchGroup != null)
                        branchGroup.lBranches.Add(branch);
                }
                return branchGroups;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to create group", ex);
                return new StatusCodeResult(400);
            }
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
                DataTable dt = await _SqlDataAccess.ExecuteDatatableSP("su_GetTargetType", null);
                List<TargetType> targetsType = _targetTypeObjectGenerator.GeneratListFromDataTable(dt);
                return targetsType;
            }
            catch (Exception ex)
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "in SettingsService, get all Task Types, failed when trying to approach the database");
                return new StatusCodeResult(400);
            }
        }
    }
}
