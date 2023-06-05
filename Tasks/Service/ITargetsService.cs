using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ITargetsService
    {
        Task<List<Target>> GetTargetsByUserId(int userId, int permissionLevelId);
        Task AddTarget(string comment, int typeTargetId, int[] personId, DateTime? targetDate, int creatorId);

    }
}
