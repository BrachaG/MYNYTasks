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
        public async Task<ActionResult<List<Survey>>> Get(string? branchId = null)
        {
            _logger.LogDebug("Get survey");
            string Sub = JwtRegisteredClaimNames.Sub;
            string permissionId = User.Claims.FirstOrDefault(c => c.Type == Sub).Value;
            return await _surveysService.GetByUserId(int.Parse(permissionId), int.Parse(branchId));
        }
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<ResultsForSurvey>> GetResultsForSurvey(int id, string? branchId = null)
        {
            _logger.LogDebug("Get ResultsForSurvey");
            string Sub = JwtRegisteredClaimNames.Sub;
            string permissionId = User.Claims.FirstOrDefault(c => c.Type == Sub).Value;
            return await _surveysService.Get(id, int.Parse(permissionId), int.Parse(branchId));
        }
    }
}
