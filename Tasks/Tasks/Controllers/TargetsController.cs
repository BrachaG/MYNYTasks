using Entities;
using Microsoft.AspNetCore.Mvc;
using Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TargetsController : ControllerBase
    {
        ILogger<TargetsController> _logger;
        ITargetsService _targetsService;
        public TargetsController(ITargetsService targetsService, ILogger<TargetsController> logger)
        {
            _logger = logger;
            _targetsService = targetsService;
        }
        [HttpGet("Get")]
        public async Task<ActionResult<List<Target>>> GetTargetsByUserId()
        {
            //var PermissionLevelId = HttpContext.Items.TryGetValue("PermissionLevelId", out var PermissionLevelIdValue) && PermissionLevelIdValue is int PermissionLevelIdInt ? PermissionLevelIdInt : -1;
            //var UserId = HttpContext.Items.TryGetValue("UserId", out var UserIdValue) && UserIdValue is int UserIdInt ? UserIdInt : -1;

            //_logger.LogDebug($"User id is: {UserId} ,PermissionLevelId is: {PermissionLevelId} In GetTargetsByUserId");

            return await _targetsService.GetTargetsByUserId(2, 2);
        }

        [HttpPost()]
        public async Task<ActionResult<string>> AddTarget(String? comment, int typeTargetId, int[]? personId, DateTime? targetDate)
        {
            //int creator = (int)HttpContext.GetRouteData().Values["UserId"];
            //int PermissionLevelId = HttpContext.Items.TryGetValue("PermissionLevelId", out var PermissionLevelIdValue) && PermissionLevelIdValue is int PermissionLevelIdInt ? PermissionLevelIdInt : -1;

            //if the user isn't manager 

            _logger.LogDebug($"Comment  is: {comment} ,TypeTargetId is: {typeTargetId} ,PersonId is: {personId} In GetTargetsByUserId");
            return await _targetsService.AddTarget(comment, typeTargetId, personId, targetDate, 2, 2);
        }
    }
}
