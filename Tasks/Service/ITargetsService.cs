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
        Task<List<Target>> GetTargetsByUserId(int UserId, int Status);
        Task AddTarget(String Comment, int TargetId, int PersonId,DateTime? TargetDate);

    }
}
