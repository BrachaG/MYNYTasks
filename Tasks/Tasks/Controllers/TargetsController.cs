using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController,Authorize]
    public class TargetsController : ControllerBase
    {
        ILogger<TargetsController> _logger;
        ITargetsService _TargetsService;
        public TargetsController(ITargetsService TargetsService, ILogger<TargetsController> logger)
        {
            _logger = logger;
            _TargetsService = TargetsService;
        }
        [HttpGet("Get")]
        public async Task<List<Target>> GetTargetsByUserId()
        {
            var Status = HttpContext.Items.TryGetValue("status", out var statusValue) && statusValue is int statusInt? statusInt : -1;
            var UserId = HttpContext.Items.TryGetValue("status", out var UserIdValue) && UserIdValue is int UserIdInt ? UserIdInt : -1;
           
            _logger.LogDebug($"User id is: {UserId} ,Status is: {Status} In GetTargetsByUserId");

            return await _TargetsService.GetTargetsByUserId(2, 1);
        }

        [HttpPost()]
        public async Task AddTarget(String Comment, int TargetId, int PersonId, DateTime? TargetDate)
        {
            _logger.LogDebug($"Comment  is: {Comment} ,TargetId is: {TargetId} ,PersonId is: {PersonId} In GetTargetsByUserId");
            await _TargetsService.AddTarget(Comment, TargetId, PersonId,TargetDate);
        }



        }
}
