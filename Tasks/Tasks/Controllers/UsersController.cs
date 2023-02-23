using Entities;
using Microsoft.AspNetCore.Mvc;
using Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        ILogger<UsersController> _logger;
        IUsersService _UsersService;
        public UsersController(IUsersService UsersService, ILogger<UsersController> logger)
        {
            _logger = logger;
            _UsersService = UsersService;
        }
        [HttpGet("Get")]
        public async Task<User> GetById(string userName, string password)
        {
            _logger.LogDebug($"User name is: {userName}  In login");
            return await _UsersService.GetById(userName, password);
        }
    }
}
