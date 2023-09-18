using Entities;
using Microsoft.AspNetCore.Mvc;

namespace Service
{
    public interface ITaskService
    {
        Task<ActionResult<List<TaskModel>>> Get(int iUserId, int permissionLevelId);
        Task<IActionResult> Add(TaskModel task, int permissionLevel, int targetType, string iCoordinatorId, string iUserId);
        Task<ActionResult<List<TaskModel>>> GetByTargetId(int targetId, int permissionLevelId);
        Task<IActionResult> Update(int permissionLevel,int taskId, int? status = null, string? comments = null);
        Task<ActionResult<List<StudentForTask>>> GetStudentForTask(int iBranchId,int iUserId, int permissionLevel);

    }
}