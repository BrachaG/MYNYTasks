namespace Entities
{
    public class User:IEntity
    {   
        public int iUserId { get; set; }
        public string nvUserName { get; set; }
        public string nvPassword { get; set; }
        public int iUserStatus { get; set; }
        public int iOrganizationId { get; set; }
        public int iPermissionLevelId { get; set; }
        public int iReportedHoursTypeId { get; set; }
        public int iContinueContactPermissionId { get; set; }
        public int iWorkerId { get; set; }
        public List<CodeTable>? lBranches { get; set; } = new List<CodeTable>();
        public int iBranchId { get; set; }
        public DateTime? dtValidityDate { get; set; }
        public DateTime? dtLatestentering { get; set; }
        public int isConcentratedReport { get; set; }
        public string nvUserMail { get; set; }
        public string nvUserPhone { get; set; }
        public int iActivityPermissionId { get; set; }
    }
}
