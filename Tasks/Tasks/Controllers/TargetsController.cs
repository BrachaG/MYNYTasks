﻿using Microsoft.AspNetCore.Mvc;
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
        [HttpGet()]
        public async Task<string> GetTargetsByUserId(int UserId, int Status)
        {
            _logger.LogDebug($"User id is: {UserId} ,Status is: {Status} In GetTargetsByUserId");

            return await _TargetsService.GetTargetsByUserId(UserId, Status);
        }



    }
}
