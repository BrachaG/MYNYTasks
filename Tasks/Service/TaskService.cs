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
        public TaskService(ISqlDataAccess SqlDataAccess, ILogger<TaskService> logger, ObjectGenerator<Tasks> taskObjectGenerator)
        {
            _SqlDataAccess = SqlDataAccess;
            _logger = logger;
            _taskObjectGenerator = taskObjectGenerator;
        }

        public async Task<IActionResult> Add( Tasks task ,string permissionLevel, string targetType, string iCoordinatorId)
        { 
            _logger.LogDebug("Add", task);
            if(permissionLevel!="1" && permissionLevel!= "2" && permissionLevel != "4")
            {
                return new StatusCodeResult(403);
            }
           if(task.iDestinationId==0)
            {
                List<SqlParameter> sp = new List<SqlParameter> 
                 {
                    new SqlParameter("iUserId", iCoordinatorId),
                    new SqlParameter("iPermissionLevelId",permissionLevel )
                 };
               DataTable dt = await _SqlDataAccess.ExecuteDatatableSP("Get_Targets",sp);
               
            }
            SqlParameter[] p = new SqlParameter[]
             {
                new SqlParameter("iPermissionLevelId",permissionLevel ),
                new SqlParameter("iUserId", iCoordinatorId),
                new SqlParameter("iDestinationId", task.iDestinationId),
                new SqlParameter("iType", task.iType),
                new SqlParameter("nvCategory", task.nvCategory),
                new SqlParameter("dtEndDate", task.dtEndDate),
                new SqlParameter("iStudentId", task.iStudentId),
                new SqlParameter("nvOrigin", task.nvOrigin),
                new SqlParameter("nvComments", task.nvComments)
             };
            return new StatusCodeResult(200); 
            _SqlDataAccess.ExecuteScalarSP("su_InsertNewTask_INS", p);
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
    }
}
