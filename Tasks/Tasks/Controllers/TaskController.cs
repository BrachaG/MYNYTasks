using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.IdentityModel.Tokens.Jwt;
namespace Tasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class TaskController : ControllerBase
    {
        ILogger<TaskController> _logger;
        ITaskService _taskService;
        public TaskController(ILogger<TaskController> logger, ITaskService taskService)
        {
            _logger = logger;
            _taskService = taskService;
        }
        [HttpPost("Add")]
        public void Add(TaskModel tasks, int targetType, string iCoordinatorId)
        {
            string Sub = JwtRegisteredClaimNames.Sub;
            string permissionLevelId = User.Claims.FirstOrDefault(c => c.Type == Sub).Value;
            string Name = JwtRegisteredClaimNames.NameId;
            string iUserId = User.Claims.FirstOrDefault(c => c.Type == Name).Value;

            _logger.LogDebug("Add Task");
            _taskService.Add(tasks, int.Parse(permissionLevelId), targetType, iCoordinatorId, iUserId);
        }
        [HttpPost("AddTaskType")]
        public void AddTaskType(string typeName)
        {
            string Sub = JwtRegisteredClaimNames.Sub;
            string permissionLevelId = User.Claims.FirstOrDefault(c => c.Type == Sub).Value;
            _logger.LogDebug("Add Task Type");
            _taskService.AddTaskType(int.Parse(permissionLevelId), typeName);
        }
        [HttpGet("Get")]
        public async Task<ActionResult<List<TaskModel>>> Get()
        {
            string Sub = JwtRegisteredClaimNames.Sub;
            string permissionLevelId = User.Claims.FirstOrDefault(c => c.Type == Sub).Value;
            string Name = JwtRegisteredClaimNames.NameId;
            string iUserId = User.Claims.FirstOrDefault(c => c.Type == Name).Value;
            _logger.LogDebug("Get All Tasks");
            return await _taskService.Get(int.Parse(iUserId), int.Parse(permissionLevelId));

        }
        [HttpGet("Get/{iTargetId}")]
        public async Task<ActionResult<List<TaskModel>>> GetByTargetId(int iTargetId)
        {
            string Sub = JwtRegisteredClaimNames.Sub;
            string permissionLevelId = User.Claims.FirstOrDefault(c => c.Type == Sub).Value;
            _logger.LogDebug("Get All Tasks");
            return await _taskService.GetByTargetId(iTargetId, int.Parse(permissionLevelId));
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(int taskId, int? status = null, string? comments = null)
        {
            string Sub = JwtRegisteredClaimNames.Sub;
            string permissionLevelId = User.Claims.FirstOrDefault(c => c.Type == Sub).Value;
            _logger.LogDebug("Update Task");
            return await _taskService.Update(int.Parse(permissionLevelId),taskId, status, comments);
        }

    }
}
