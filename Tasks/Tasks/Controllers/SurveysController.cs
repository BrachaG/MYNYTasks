using Entities;
using Microsoft.AspNetCore.Mvc;
using Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Tasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController,Authorize]
    public class SurveysController : ControllerBase
    {
        ISurveysService _SurveysService;

        public SurveysController(ISurveysService SurveysService)
        {
            _SurveysService = SurveysService;

        }
        [HttpGet("Get")]
        public async Task<List<Survey>> Get()
        {
            var user = User.Claims;
            var a = user.Where(u => u.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Select(c => c.Value);    
            string userId = a.ToList()[0];
            return await _SurveysService.Get(userId);
        }
    }
}
