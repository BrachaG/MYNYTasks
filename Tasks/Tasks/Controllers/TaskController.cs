using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities;
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
        public void Add(Entities.Tasks tasks, string targetType, string iCoordinatorId)
        {
            string Sub = JwtRegisteredClaimNames.Sub;
            string permissionLevelId = User.Claims.FirstOrDefault(c => c.Type == Sub).Value;

            _logger.LogDebug("Add Task");
            _taskService.Add(tasks, permissionLevelId, targetType, iCoordinatorId);
        }
        [HttpGet("Get")]
        public async Task<List<Entities.Tasks>> Get()
        {
            string Sub = JwtRegisteredClaimNames.Sub;
            string permissionLevelId = User.Claims.FirstOrDefault(c => c.Type == Sub).Value;
            string Name = JwtRegisteredClaimNames.NameId;
            string iUserId = User.Claims.FirstOrDefault(c => c.Type == Name).Value;
            _logger.LogDebug("Get All Tasks");
            return await _taskService.Get(int.Parse(iUserId), int.Parse(permissionLevelId));
        }
         [HttpGet("Get/{iTargetId}")]
         public async Task<List<Entities.Tasks>> GetByTargetId(int iTargetId)
        {
            string Sub = JwtRegisteredClaimNames.Sub;
            string permissionLevelId = User.Claims.FirstOrDefault(c => c.Type == Sub).Value;
            _logger.LogDebug("Get All Tasks");
            return await _taskService.GetByTargetId(iTargetId,int.Parse(permissionLevelId));
        }

    }
}
