using Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ITaskService
    {
        Task<List<Tasks>> Get(int iUserId, int permissionLevelId);
        Task<IActionResult> Add(Tasks task, string permissionLevel, int targetType, string iCoordinatorId);
       Task<Tasks> GetByTargetId(int targetId, int permissionLevelId);
    }
}
