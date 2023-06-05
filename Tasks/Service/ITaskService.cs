using Entities;
using Microsoft.AspNetCore.Mvc;

namespace Service
{
    public interface ITaskService
    {
        Task<ActionResult<List<Tasks>>> Get(int iUserId, int permissionLevelId);
        Task<IActionResult> Add(Tasks task, int permissionLevel, int targetType, string iCoordinatorId, string iUserId);
        Task<ActionResult<List<Tasks>>> GetByTargetId(int targetId, int permissionLevelId);
        Task<IActionResult> Update(int permissionLevel, int? status = null, string? comments = null);
        Task<IActionResult> AddTaskType(int permissionLevelId, string typeName);
    }
}