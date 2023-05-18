using Entities;
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
    public class TaskService: ITaskService
    {
        ISqlDataAccess _SqlDataAccess;
        ILogger<TaskService> _logger;

        public TaskService(ISqlDataAccess SqlDataAccess, ILogger<TaskService> logger)
        {
            _SqlDataAccess = SqlDataAccess;
            _logger = logger;
        }

        public void Add(Tasks task, int iUserId)
        { 
            _logger.LogDebug("Add", task);
            SqlParameter[] p = new SqlParameter[]
             {
                new SqlParameter("iUserId", iUserId),
                new SqlParameter("iDestinationId", task.iDestinationId),
                new SqlParameter("iType", task.iType),
                new SqlParameter("nvCategory", task.nvCategory),
                new SqlParameter("dtEndDate", task.dtEndDate),
                new SqlParameter("iStatus", 1),
                new SqlParameter("iStudentId", task.iStudentId),
                new SqlParameter("nvOrigin", task.nvOrigin),
                new SqlParameter("nvComments", task.nvComments)
             };
            _SqlDataAccess.ExecuteScalarSP("su_InsertNewTask_INS", p);
        }

        public Task<List<Tasks>> Get()
        {
            throw new NotImplementedException();
        }
    }
}
