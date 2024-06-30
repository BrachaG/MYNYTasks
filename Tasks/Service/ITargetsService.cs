using Entities;
using Microsoft.AspNetCore.Mvc;


namespace Service
{
    public interface ITargetsService
    {
        Task<ActionResult<List<Target>>> GetTargetsByTargetType(int userId, int permissionLevelId, int targetType);
        Task<ActionResult<List<Target>>> GetTargetsByUserId(int userId, int permissionLevelId);
        Task<ActionResult<string>> AddTarget(string comment, int typeTargetId, int[] personId, int BranchId, DateTime? targetDate, int creatorId, int permissionLevelId);
        void SendEmail(string recipient, string subject, string body);
    }
}
