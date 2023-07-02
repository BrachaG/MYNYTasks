﻿using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
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
            int userId = HttpContext.Items.TryGetValue("UserId", out var UserIdValue) && UserIdValue is int UserIdInt ? UserIdInt : -1;
            int permissionLevelId = HttpContext.Items.TryGetValue("PermissionLevelId", out var permissionLevelIdValue) && permissionLevelIdValue is int permissionLevelIdInt ? permissionLevelIdInt : -1;
            _logger.LogDebug($"User id is: {userId} ,PermissionLevelId is: {permissionLevelId} In GetTargetsByUserId");
            return await _targetsService.GetTargetsByUserId(userId, permissionLevelId);
        }

        [HttpPost()]
        public async Task<ActionResult<string>> AddTarget(String? comment, int typeTargetId, int[]? personId, int branchId, DateTime? targetDate)
        {
            //int permissionLevelId = HttpContext.Items.TryGetValue("PermissionLevelId", out var permissionLevelIdValue) && permissionLevelIdValue is int permissionLevelIdInt ? permissionLevelIdInt : -1;
            string Sub = JwtRegisteredClaimNames.Sub;
            var permissionLevelId = User.Claims.FirstOrDefault(c => c.Type == Sub).Value;
            string Name = JwtRegisteredClaimNames.Name;
            //var userId = User.Claims.FirstOrDefault(c => c.Type == Name).Value;// int creator = (int)HttpContext.GetRouteData().Values["UserId"];
            _logger.LogDebug($"Comment  is: {comment} ,TypeTargetId is: {typeTargetId} ,PersonId is: {personId} In GetTargetsByUserId");
            return await _targetsService.AddTarget(comment, typeTargetId, personId, branchId, targetDate, 1, int.Parse(permissionLevelId));
        }
    }
}