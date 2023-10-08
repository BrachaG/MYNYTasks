namespace Entities
{
    public class TaskModel : IEntity
    {
        public int iTaskId { get; set; }
        public int iTargetId { get; set; }
        public int iType { get; set; }
        public string nvCategory { get; set; }
        public DateTime dtEndDate { get; set; }
        public int iStatus { get; set; }
        public int? iStudentId { get; set; }
        public string nvOrigin { get; set; }
        public string? nvComments { get; set; }
    }
}
