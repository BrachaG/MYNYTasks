using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository;
using System.Data;
using System.Data.SqlClient;

namespace Service
{
    public enum Permission
    {
        Manager = 1,
        Coordinator = 2,
        SystemAdministrator = 4
    }
    public class TaskService : ITaskService
    {
        ISqlDataAccess _SqlDataAccess;
        ILogger<TaskService> _logger;
        ObjectGenerator<Tasks> _taskObjectGenerator;
        ObjectGenerator<Target> _targetObjectGenerator;
        public TaskService(ISqlDataAccess SqlDataAccess, ILogger<TaskService> logger, ObjectGenerator<Tasks> taskObjectGenerator, ObjectGenerator<Target> targetObjectGenerator)
        {
            _SqlDataAccess = SqlDataAccess;
            _logger = logger;
            _taskObjectGenerator = taskObjectGenerator;
            _targetObjectGenerator = targetObjectGenerator;
        }
        public async Task<IActionResult> Add(Tasks task, int permissionLevel, int targetType, string iCoordinatorId, string iUserId)
        {
            _logger.LogDebug("Add", task);
            if ((Permission)permissionLevel != Permission.Manager && (Permission)permissionLevel != Permission.Coordinator && (Permission)permissionLevel != Permission.SystemAdministrator)
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
                    DataTable dt = await _SqlDataAccess.ExecuteDatatableSP("Get_Targets", sp);
                    List<Target> targets = _targetObjectGenerator.GeneratListFromDataTable(dt);
                    Target? target = targets.FirstOrDefault(t => t.iTargetId == targetType);
                    if (target != null)
                    {
                        task.iTargetId = target.iId;
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
                            await _SqlDataAccess.ExecuteDatatableSP("Insert_Target", parameters);
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
                await _SqlDataAccess.ExecuteScalarSP("su_InsertNewTask_INS", p);
            }
            catch (Exception ex)
            {
                _logger.LogError("failed to insert task", ex);
                return new StatusCodeResult(400);
            }
            return new StatusCodeResult(200);
        }

        public async Task<IActionResult> AddTaskType(int permissionLevelId, string typeName)
        {
            if ((Permission)permissionLevelId != Permission.Manager && (Permission)permissionLevelId != Permission.SystemAdministrator)
            {
                return new StatusCodeResult(403);
            }
            SqlParameter[] p = new SqlParameter[]
            {
                new SqlParameter("iPermissionLevelId",permissionLevelId ),
                new SqlParameter("nvTypeName", typeName)
            };
            try
            {
                await _SqlDataAccess.ExecuteScalarSP("su_AddTaskType_INS", p);
            }
            catch (Exception ex)
            {
                _logger.LogError("failed to insert task type", ex);
                return new StatusCodeResult(400);
            }
            return new StatusCodeResult(200);
        }

        public async Task<ActionResult<List<Tasks>>> Get(int iUserId, int permissionLevelId)
        {
            List<SqlParameter> sp = new List<SqlParameter>
                 {
                    new SqlParameter("iUserId", iUserId),
                    new SqlParameter("iPermissionLevelId",permissionLevelId )
                 };
            try
            {
                DataTable dt = await _SqlDataAccess.ExecuteDatatableSP("su_GetTasks_GET", sp);
                List<Tasks> tasks = _taskObjectGenerator.GeneratListFromDataTable(dt);
                return tasks;
            }
            catch (Exception ex)
            {
                _logger.LogError("failed to get task", ex);
                return new StatusCodeResult(400);
            }

        }

        public async Task<ActionResult<List<Tasks>>> GetByTargetId(int iTargetId, int permissionLevel)
        {
            if ((Permission)permissionLevel != Permission.Manager && (Permission)permissionLevel != Permission.Coordinator && (Permission)permissionLevel != Permission.SystemAdministrator)
            {
                return new StatusCodeResult(403);
            }
            List<SqlParameter> sp = new List<SqlParameter>
                 {
                    new SqlParameter("iTargetId", iTargetId),
                    new SqlParameter("iPermissionLevelId",permissionLevel)
                 };
            try
            {
                DataTable dt = await _SqlDataAccess.ExecuteDatatableSP("su_GetTaskByTargetId", sp);
                List<Tasks> tasks = _taskObjectGenerator.GeneratListFromDataTable(dt);
                return tasks;
            }
            catch (Exception ex)
            {
                _logger.LogError("failed to get task by tsrgetId", ex);
                return new StatusCodeResult(400);
            }
        }
        public async Task<IActionResult> Update(int permissionLevel, int? status = null, string? comments = null)
        {
            if ((Permission)permissionLevel != Permission.Manager && (Permission)permissionLevel != Permission.Coordinator && (Permission)permissionLevel != Permission.SystemAdministrator)
            {
                return new StatusCodeResult(403);
            }
            SqlParameter[] p = new SqlParameter[]
              {
                    new SqlParameter("iPermissionLevelId",permissionLevel),
                    new SqlParameter("iStutusId", status),
                    new SqlParameter("nvComments",comments)
              };
            try
            {
                await _SqlDataAccess.ExecuteScalarSP("su_UpdateTask_UPD", p);
                return new StatusCodeResult(200);
            }
            catch
            {
                return new StatusCodeResult(400);
            }

        }
    }
}
