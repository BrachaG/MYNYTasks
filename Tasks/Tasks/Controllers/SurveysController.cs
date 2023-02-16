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
            return await _SurveysService.Get();
        }
    }
}
