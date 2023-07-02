using Entities;

namespace Service
{
    public interface ITargetsService
    {
        Task<List<Target>> GetTargetsByUserId(int UserId, int PermissionLevelId);
        Task AddTarget(string comment, int typeTargetId, int[] personId, DateTime? targetDate, int creatorId);
        void SendEmail(string recipient, string subject, string body);
    }
}
