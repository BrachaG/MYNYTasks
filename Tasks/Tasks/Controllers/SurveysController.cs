using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Service;

namespace Tasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class SurveysController : ControllerBase
    {
        ISurveysService _surveysService;
        ILogger<SurveysController> _logger;
        public SurveysController(ISurveysService surveysService, ILogger<SurveysController> logger)
        {

            _surveysService = surveysService;
            _logger = logger;

        }
        [HttpGet("Get")]
        public async Task<List<Survey>> Get()
        {
            _logger.LogDebug("Get survey");
            string Type = JwtRegisteredClaimNames.Sub;
            string userId = User.Claims.FirstOrDefault(c => c.Type == Type).Value;
            return await _surveysService.Get();

            ;
        }
    }
}
