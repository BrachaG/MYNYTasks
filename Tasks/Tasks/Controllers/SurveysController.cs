using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
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
        public async Task<DataTable> GetSurveysByUserId()
        {
            return await _SurveysService.GetSurveysByUserId();
        }
    }
}
