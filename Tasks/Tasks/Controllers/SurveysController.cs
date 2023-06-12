using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Service;

namespace Tasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]//, Authorize]
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
            string Type = JwtRegisteredClaimNames.NameId;
            string userId = User.Claims.FirstOrDefault(c => c.Type == Type).Value;
            return await _SurveysService.Get();
        }
        [HttpGet("Get/{id}")]
        public async Task<ResultsForSurvey> GetResultsForSurvey(int id)
        {
            _logger.LogDebug("Get ResultsForSurvey");

            return await _SurveysService.Get(id);
        }
        [HttpGet("GetImage/{id}")]
        public async Task<IActionResult> GetImage(string id)
        {
            _logger.LogDebug("Get Image By User Id");

            return _SurveysService.GetImage(id);
        }
    }
}
