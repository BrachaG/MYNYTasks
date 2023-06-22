namespace Entities
{
    public class BranchGroup : IEntity
    {
        public int iGroupId { get; set; }
        public string nvGroupName { get; set; }
        public List<Branch> lBranches { get; set; } = new List<Branch>();
    }
}
