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
        public SettingsController(ISettingsService settingsService, ILogger<SettingsController> logger)
        {
            _logger = logger;
            _settingsService = settingsService;
        }
        [HttpGet]
        public List<TaskType> GetTasksType()
        {
            return new List<TaskType>();
        }
        [HttpPost]
        public async Task AddTaskType(string name)
        {
            _logger.LogDebug("Add Task Type");
            string Sub = JwtRegisteredClaimNames.Sub;
            string permissionId = User.Claims.FirstOrDefault(c => c.Type == Sub).Value;
            await _settingsService.AddTaskType(name, permissionId);
        }

    }
}
