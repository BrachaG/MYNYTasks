using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.IdentityModel.Tokens.Jwt;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class SettingsController : ControllerBase
    {
        ISettingsService _settingsService;
        ILogger<SettingsController> _logger;
        readonly string permissionId;
        readonly string userId;
        public SettingsController(ISettingsService settingsService, ILogger<SettingsController> logger)
        {
            _logger = logger;
            _settingsService = settingsService;
            string Sub = JwtRegisteredClaimNames.Sub;
            permissionId = User.Claims.FirstOrDefault(c => c.Type == Sub).Value;
            string Name = JwtRegisteredClaimNames.Name;
            userId = User.Claims.FirstOrDefault(c => c.Type == Name).Value;
        }
        [HttpGet("GetTasksType")]
        public async Task<ActionResult<List<TaskType>>> GetTasksType()
        {
            _logger.LogDebug("get Task Type");
            return await _settingsService.GetTaskTypes();
        }
        [HttpPost("AddTaskType")]
        public async Task AddTaskType(string name)
        {
            _logger.LogDebug("Add Task Type");
            await _settingsService.AddTaskType(name, permissionId);
        }
        [HttpPost("AddTargetType")]
        public async Task<IActionResult> AddTargetType(string name)
        {
            _logger.LogDebug("Add Target Type");
            return await _settingsService.AddTargetType(name, permissionId,userId);
        }
        [HttpPost("CreateBranchGroup")]
        public async Task<IActionResult> CreateBranchGroup(string name)
        {
            _logger.LogDebug("Create Branch Group");
            return await _settingsService.CreateBranchGroup(name, permissionId);
        }
        [HttpPost("AddStatus")]
        public async Task<IActionResult> AddStatus(string name)
        {
            _logger.LogDebug("Add Status");
            return await _settingsService.AddStatus(name, permissionId,userId);
        }

    }
}
