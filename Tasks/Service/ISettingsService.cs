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
        Task<IActionResult> AddTargetType( string name);
        Task<IActionResult> AddTaskType( string name, string permissionLevel);
        Task<IActionResult> CreateBranchGroup(string name);
        Task<IActionResult> AddStatus(string name);
    }
}
