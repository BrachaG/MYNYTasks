using Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ITargetsService
    {
        Task<ActionResult<List<Target>>> GetTargetsByUserId(int userId, int permissionLevelId);
        Task<ActionResult<string>> AddTarget(string comment, int typeTargetId, int[] personId, int BranchId, DateTime? targetDate, int creatorId,int PermissionLevelId);

    }
}
