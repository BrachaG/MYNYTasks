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
    public class TaskService: ITaskService
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

        public async Task<IActionResult> Add( Tasks task ,string permissionLevel, int targetType, string iCoordinatorId)
        { 
            _logger.LogDebug("Add", task);
            if(permissionLevel!="1" && permissionLevel!= "2" && permissionLevel != "4")
            {
                return new StatusCodeResult(403);
            }
           if(task.iTargetId == 0)
            {
                List<SqlParameter> sp = new List<SqlParameter> 
                 {
                    new SqlParameter("iUserId", iCoordinatorId),
                    new SqlParameter("iPermissionLevelId",permissionLevel )
                 };
               DataTable dt = await _SqlDataAccess.ExecuteDatatableSP("Get_Targets",sp);
               List<Target> targets = _targetObjectGenerator.GeneratListFromDataTable(dt);
                Target target = targets.FirstOrDefault(t => t.iTargetId == targetType);
                if (target != null)
                {
                    task.iTargetId = target.iId;
                }
                else
                {
                    //add new target....
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
            _SqlDataAccess.ExecuteScalarSP("su_InsertNewTask_INS", p);
            return new StatusCodeResult(200);
        }

        public async Task<List<Tasks>> Get(int iUserId, int permissionLevelId)
        {
            List<SqlParameter> sp = new List<SqlParameter>
                 {
                    new SqlParameter("iUserId", iUserId),
                    new SqlParameter("iPermissionLevelId",permissionLevelId )
                 };
            DataTable dt = await _SqlDataAccess.ExecuteDatatableSP("su_GetTasks_GET", sp);
            List<Tasks> tasks = _taskObjectGenerator.GeneratListFromDataTable(dt);
            return tasks;
        }

        public async Task<Tasks> GetByTargetId(int iTargetId, int iPermissionLevelId)
        {
           if(iPermissionLevelId != 1 && iPermissionLevelId != 2 && iPermissionLevelId != 4)
            {
                return null;
            }
            SqlParameter [] sp = new SqlParameter[]
                 {
                    new SqlParameter("iTargetId", iTargetId),
                    new SqlParameter("iPermissionLevelId",iPermissionLevelId)
                 };
            Object dr = await _SqlDataAccess.ExecuteScalarSP("su_GetTaskByTargetId", sp);
            Tasks task= _taskObjectGenerator.GeneratFromDataRow((DataRow)dr);
            return task;
        }
        public async Task<IActionResult> Update(int permissionLevel, int? status=null, DateTime? endDate=null, string? comments=null)
        {
          SqlParameter[] p = new SqlParameter[]
          {
                new SqlParameter("iPermissionLevelId",permissionLevel),
                new SqlParameter("dtEndDate", endDate),
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
