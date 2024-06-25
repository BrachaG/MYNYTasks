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
        IUsersService _usersService;
        public UsersController(IUsersService usersService, ILogger<UsersController> logger)
        {
            _logger = logger;
            _usersService = usersService;
        }
        [HttpGet("Get")]
        public async Task<ActionResult<User>> GetById(string userName, string password)
        {
            _logger.LogDebug($"user name is: {userName}  In login");
            return await _usersService.GetById(userName, password);
        }

    }
}
