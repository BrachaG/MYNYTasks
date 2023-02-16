using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Tasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class SurveysController : ControllerBase
    {
        ISurveysService _SurveysService;
        IConfiguration _Configuration;

        public SurveysController(ISurveysService SurveysService, IConfiguration Configuration)
        {
            _SurveysService = SurveysService;

        }
        [HttpGet("Get")]
        public async Task<List<Survey>> Get()
        {

            var user = User.Claims;
            string? userId = null;
            if (user != null)
            {
                string nameIdentifierClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
                var nameIdentifierClaim = user.FirstOrDefault(c => c.Type == nameIdentifierClaimType);
                if (nameIdentifierClaim != null)
                {
                    userId = nameIdentifierClaim.Value;
                }
                else
                {
                    throw new Exception();
                    // handle case where nameidentifier claim is missing
                }
            }
            else
            {
                throw new Exception();
                // handle case where user claims are missing
            }
            return await _SurveysService.Get(userId);
        }
    }
}
