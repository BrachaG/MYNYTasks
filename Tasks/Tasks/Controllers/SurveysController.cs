using Entities;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Tasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveysController : ControllerBase
    {
        ISurveysService _SurveysService;

        public SurveysController(ISurveysService SurveysService)
        {
            _SurveysService = SurveysService;

        }
        [HttpGet("GetSurveysByUserId")]
        public async Task<List<Survey>> GetSurveysByUserId()
        {
            return await _SurveysService.Get();
        }
    }
}
