namespace Entities
{
    public class Branch : IEntity
    {
        public int iBranchId { get; set; }
        public string nvBranchName { get; set; }
        public int iGroupId { get; set; }
    }
}
