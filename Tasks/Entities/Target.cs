namespace Entities
{
    public class Target : IEntity
    {
        public int iTargetId { get; set; }
        public string nvComment { get; set; }
        public int iTypeTargetId { get; set; }
        public int iPersonId { get; set; }
        public int iBranchId { get; set; }
        public DateTime? dtTargetDate { get; set; }
        public int iStatusId { get; set; }
        public int iCreatorId { get; set; }
        public DateTime dtCreation { get; set; }
    }
}
