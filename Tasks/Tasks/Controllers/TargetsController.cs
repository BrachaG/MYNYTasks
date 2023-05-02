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
        [HttpGet("{id}")]
        public async string GetTargetsByUserId(int UserId)
        {
            _logger.LogDebug($"User id is: {UserId}  In GetTargetsByUserId");

            return await _TargetsService.GetTargetsByUserId(UserId);
        }

     
    }
}
