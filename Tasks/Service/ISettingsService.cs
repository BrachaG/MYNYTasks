using Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ISettingsService
    {
        Task<ActionResult<List<TargetType>>> GetTargetTypes();
        Task<IActionResult> AddTargetType( string name, string permissionLevel, string userId);
        Task<ActionResult<List<TaskType>>> GetTaskTypes();
        Task<IActionResult> AddTaskType( string name, string permissionLevel);
        Task<ActionResult<List<BrancheGroup>>> GetBranchGroup();
        Task<IActionResult> CreateBranchGroup(string name, string permissionLevel);
        Task<ActionResult<List<TargetStatus>>> GetStatuses();
        Task<IActionResult> AddStatus(string name, string permissionLevel, string userId);
    }
}
