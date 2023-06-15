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
        ISurveysService _SurveysService;
        ILogger<SurveysController> _logger;
        public SurveysController(ISurveysService SurveysService, ILogger<SurveysController> logger)
        {
            _SurveysService = SurveysService;
            _logger = logger;
        }
        [HttpGet("Get")]
        public async Task<List<Survey>> Get()
        {
            _logger.LogDebug("Get survey");
            string Sub = JwtRegisteredClaimNames.Sub;
            string permissionId = User.Claims.FirstOrDefault(c => c.Type == Sub).Value;
            string Name = JwtRegisteredClaimNames.NameId;
            string userId = User.Claims.FirstOrDefault(c => c.Type == Name).Value;
            return await _SurveysService.GetByUserId(userId, permissionId);
        }
        [HttpGet("Get/{id}")]
        public async Task<ResultsForSurvey> GetResultsForSurvey(int id)
        {
            _logger.LogDebug("Get ResultsForSurvey");

            return await _SurveysService.Get(id);
        }
    }
}
