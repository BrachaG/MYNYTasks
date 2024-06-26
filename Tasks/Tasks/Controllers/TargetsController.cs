using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Service;
using System.IdentityModel.Tokens.Jwt;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class TargetsController : ControllerBase
    {
        ILogger<TargetsController> _logger;
        ITargetsService _targetsService;
/*        int creator;
        int permissionLevelId;
        int userId;*/
        public TargetsController(ITargetsService targetsService, ILogger<TargetsController> logger)
        {
            _logger = logger;
            _targetsService = targetsService;
           /* creator = (int)HttpContext.GetRouteData().Values["UserId"];
            permissionLevelId =  HttpContext.Items.TryGetValue("PermissionLevelId", out var permissionLevelIdValue) && permissionLevelIdValue is int permissionLevelIdInt ? permissionLevelIdInt : -1;
            userId = HttpContext.Items.TryGetValue("UserId", out var UserIdValue) && UserIdValue is int UserIdInt ? UserIdInt : -1;*/
        }
        [HttpGet("Get/{TargetType}")]
        public async Task<ActionResult<List<Target>>> GetTargetsByTargetType(int targetType)
        {
            string Type = JwtRegisteredClaimNames.Sub;
            string userId = User.Claims.FirstOrDefault(c => c.Type == Type).Value;
            string Name = JwtRegisteredClaimNames.NameId;
            string permissionLevelId = User.Claims.FirstOrDefault(c => c.Type == Name).Value;
            _logger.LogDebug($"User id is: {userId} ,PermissionLevelId is: {permissionLevelId} In GetTargetsByUserId");
            return await _targetsService.GetTargetsByTargetType(int.Parse(userId), int.Parse(permissionLevelId), targetType);
        }

        [HttpGet("GetTargetsByUserId")]
        public async Task<ActionResult<List<Target>>> GetTargetsByUserId()
        {
            string Name = JwtRegisteredClaimNames.NameId;
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == Name).Value);
            var permissionLevelId = HttpContext.Items.TryGetValue("PermissionLevelId", out var permissionLevelIdValue) && permissionLevelIdValue is int permissionLevelIdInt ? permissionLevelIdInt : -1;
            _logger.LogDebug($"User id is: {userId} ,PermissionLevelId is: {permissionLevelId} In GetTargetsByUserId");
            return await _targetsService.GetTargetsByUserId(userId, permissionLevelId);
        }

        [HttpPost()]
        public async Task<ActionResult<string>> AddTarget(String? comment, int typeTargetId, int[]? personId, int branchId, DateTime? targetDate)
        {
            string Type = JwtRegisteredClaimNames.Sub;
            string userId = User.Claims.FirstOrDefault(c => c.Type == Type).Value;
            string Name = JwtRegisteredClaimNames.NameId;
            string permissionLevelId = User.Claims.FirstOrDefault(c => c.Type == Name).Value;
            _logger.LogDebug($"Comment  is: {comment} ,TypeTargetId is: {typeTargetId} ,PersonId is: {personId} In GetTargetsByUserId");
            return await _targetsService.AddTarget(comment, typeTargetId, personId, branchId, targetDate, int.Parse(userId), int.Parse(permissionLevelId));
        }
    }
}