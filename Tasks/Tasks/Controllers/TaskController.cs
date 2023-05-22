using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities;
using Service;
using System.IdentityModel.Tokens.Jwt;

namespace Tasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController ,Authorize]
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
        public void Add(Entities.Tasks tasks)
        {
            string Type = JwtRegisteredClaimNames.Sub;
            string userId = User.Claims.FirstOrDefault(c => c.Type == Type).Value;
            _logger.LogDebug("Add Task");
            _taskService.Add(tasks, userId);
        }
    }
}
