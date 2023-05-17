﻿namespace Entities
{
    public class User : IEntity
    {
        public int iUserId { get; set; }
        public string nvUserName { get; set; }
        public int iPermissionLevelId { get; set; }      
        public int iContinueContactPermissionId { get; set; }
        public List<CodeTable>? lBranches { get; set; } = new List<CodeTable>();
        public int iBranchId { get; set; }    
        public int iActivityPermissionId { get; set; }
        public string token { get; set; }

    }
}
