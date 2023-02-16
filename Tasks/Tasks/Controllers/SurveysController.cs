using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Service;
using ServiceStack.Host;
using System.Net;


namespace Tasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
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

            string Type = JwtRegisteredClaimNames.Sub;
            var user = User.Claims.FirstOrDefault(c => c.Type == Type);
            string? userId = null;
            if (user != null)
            {
                userId = user.Value;
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                throw new HttpException();

            }
            return await _SurveysService.Get(userId);
        }
    }
}
