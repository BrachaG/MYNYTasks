using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository;
using System.Data;
using System.Data.SqlClient;
using static Service.PermissionLevelEnum;

namespace Service
{
    public class TaskService : ITaskService
    {
        ISqlDataAccess _sqlDataAccess;
        ILogger<TaskService> _logger;
        IObjectGenerator<TaskModel> _taskObjectGenerator;
        IObjectGenerator<Target> _targetObjectGenerator;
        public TaskService(ISqlDataAccess sqlDataAccess, ILogger<TaskService> logger, IObjectGenerator<TaskModel> taskObjectGenerator, IObjectGenerator<Target> targetObjectGenerator)
        {
            _sqlDataAccess = sqlDataAccess;
            _logger = logger;
            _taskObjectGenerator = taskObjectGenerator;
            _targetObjectGenerator = targetObjectGenerator;
        }
        public async Task<IActionResult> Add(TaskModel task, int permissionLevel, int targetType, string iCoordinatorId, string iUserId)
        {
            _logger.LogDebug("Add", task);
            if ((PermissionLevel)permissionLevel != PermissionLevel.NYmanagar && (PermissionLevel)permissionLevel != PermissionLevel.coordinator && (PermissionLevel)permissionLevel != PermissionLevel.SystemManager)
            {
                return new StatusCodeResult(403);
            }
            if (task.iTargetId == 0)
            {
                List<SqlParameter> sp = new List<SqlParameter>
                 {
                    new SqlParameter("iUserId", iCoordinatorId),
                    new SqlParameter("iPermissionLevelId",permissionLevel )
                 };
                try
                {
                    DataTable dt = await _sqlDataAccess.ExecuteDatatableSP("Get_Targets", sp);
                    List<Target> targets = _targetObjectGenerator.GeneratListFromDataTable(dt);
                    Target? target = targets.FirstOrDefault(t => t.iTargetId == targetType);
                    if (target != null)
                    {
                        task.iTargetId = target.iTypeTargetId;
                    }
                    else
                    {
                        DataTable personIds = new DataTable();
                        personIds.Columns.Add("Id", typeof(int));
                        personIds.Rows.Add(iCoordinatorId);

                        List<SqlParameter> parameters = new List<SqlParameter>
                         {
                            new SqlParameter
                            {
                                ParameterName = "Ids",
                                SqlDbType = SqlDbType.Structured,
                                TypeName = "dbo.PersonIds",
                                Value = personIds
                            },
                            new SqlParameter("CreatorId", iUserId),
                            new SqlParameter("typeTargetId", targetType)
                         };
                        try
                        {
                            await _sqlDataAccess.ExecuteDatatableSP("Insert_Target", parameters);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError("Failed to insert target", ex);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("failed to get targets", ex);
                    return new StatusCodeResult(400);
                }

            }
            SqlParameter[] p = new SqlParameter[]
             {
                new SqlParameter("iPermissionLevelId",permissionLevel ),
                new SqlParameter("iTargetId", task.iTargetId),
                new SqlParameter("iType", task.iType),
                new SqlParameter("nvCategory", task.nvCategory),
                new SqlParameter("dtEndDate", task.dtEndDate),
                new SqlParameter("iStudentId", task.iStudentId),
                new SqlParameter("nvOrigin", task.nvOrigin),
                new SqlParameter("nvComments", task.nvComments)
             };
            try
            {
                await _sqlDataAccess.ExecuteScalarSP("su_InsertNewTask_INS", p);
            }
            catch (Exception ex)
            {
                _logger.LogError("failed to insert task", ex);
                return new StatusCodeResult(400);
            }
            return new StatusCodeResult(200);
        }

        public async Task<ActionResult<List<TaskModel>>> Get(int iUserId, int permissionLevelId)
        {
            List<SqlParameter> sp = new List<SqlParameter>
                 {
                    new SqlParameter("iUserId", iUserId),
                    new SqlParameter("iPermissionLevelId",permissionLevelId )
                 };
            try
            {
                DataTable dt = await _sqlDataAccess.ExecuteDatatableSP("su_GetTasks_GET", sp);
                List<TaskModel> tasks = _taskObjectGenerator.GeneratListFromDataTable(dt);
                foreach (DataColumn column in dt.Columns)
                {
                    Console.WriteLine(column.ColumnName);
                }

                Console.WriteLine("Row count: " + dt.Rows.Count);
                return tasks;
            }
            catch (Exception ex)
            {
                _logger.LogError("failed to get task", ex);
                return new StatusCodeResult(400);
            }

        }

        public async Task<ActionResult<List<TaskModel>>> GetByTargetId(int iTargetId, int permissionLevel)
        {
            if ((PermissionLevel)permissionLevel != PermissionLevel.NYmanagar && (PermissionLevel)permissionLevel != PermissionLevel.coordinator && (PermissionLevel)permissionLevel != PermissionLevel.SystemManager)
            {
                return new StatusCodeResult(403);
            }
            List<SqlParameter> sp = new List<SqlParameter>
                 {
                    new SqlParameter("iTargetId", iTargetId),
                 };
            try
            {
                DataTable dt = await _sqlDataAccess.ExecuteDatatableSP("su_GetTaskByTargetId", sp);
                List<TaskModel> tasks = _taskObjectGenerator.GeneratListFromDataTable(dt);
                return tasks;
            }
            catch (Exception ex)
            {
                _logger.LogError("failed to get task by tsrgetId", ex);
                return new StatusCodeResult(400);
            }
        }
        public async Task<IActionResult> Update(int permissionLevel, int taskId, int? status = null, string? comments = null)
        {
            if ((PermissionLevel)permissionLevel != PermissionLevel.NYmanagar && (PermissionLevel)permissionLevel != PermissionLevel.coordinator && (PermissionLevel)permissionLevel != PermissionLevel.SystemManager)
            {
                return new StatusCodeResult(403);
            }
            SqlParameter[] p = new SqlParameter[]
              {
                    new SqlParameter("iPermissionLevelId",permissionLevel),
                    new SqlParameter("iTaskId", taskId),
                    new SqlParameter("iStatusId", status),
                    new SqlParameter("nvComments",comments)
              };
            try
            {
                await _sqlDataAccess.ExecuteScalarSP("su_UpdateTask_UPD", p);
                return new StatusCodeResult(200);
            }
            catch (Exception ex)
            {
                _logger.LogError("failed to update task", ex);
                return new StatusCodeResult(400);
            }
            return new StatusCodeResult(400);
            }

        }
    }

