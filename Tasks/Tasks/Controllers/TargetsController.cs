﻿using Entities;
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
        ITargetsService _TargetsService;
        public TargetsController(ITargetsService TargetsService, ILogger<TargetsController> logger)
        {
            _logger = logger;
            _TargetsService = TargetsService;
        }
        [HttpGet("Get")]
        public async Task<List<Target>> GetTargetsByUserId()
        {
            var PermissionLevelId = HttpContext.Items.TryGetValue("PermissionLevelId", out var PermissionLevelIdValue) && PermissionLevelIdValue is int PermissionLevelIdInt ? PermissionLevelIdInt : -1;
            var UserId = HttpContext.Items.TryGetValue("UserId", out var UserIdValue) && UserIdValue is int UserIdInt ? UserIdInt : -1;

            _logger.LogDebug($"User id is: {UserId} ,PermissionLevelId is: {PermissionLevelId} In GetTargetsByUserId");

            return await _TargetsService.GetTargetsByUserId(UserId, PermissionLevelId);
        }

        [HttpPost()]
        public async Task AddTarget(String? Comment, int TypeTargetId, int[]? PersonId, DateTime? TargetDate)
        {
            int creator = 2;
                //(int)HttpContext.GetRouteData().Values["UserId"];
            //if the user isn't manager 
            if (PersonId == null)
            {
                PersonId[0] = creator;
            }
            _logger.LogDebug($"Comment  is: {Comment} ,TypeTargetId is: {TypeTargetId} ,PersonId is: {PersonId} In GetTargetsByUserId");
            await _TargetsService.AddTarget(Comment, TypeTargetId, PersonId, TargetDate, creator);
        }
    }
}
