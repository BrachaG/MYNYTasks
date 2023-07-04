using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Target : IEntity
    {
        public int iTargetId { get; set; }
        public string nvComment { get; set; }
        public int itypeTargetId { get; set; }
        public int iPersonId { get; set; }
        public int iBranchId { get; set; }
        public DateTime? dtTargetDate { get; set; }
        public int iStatusId { get; set; }
        public int iCreatorId { get; set; }
        public DateTime dtCreation { get; set; }
        public string nvUserName { get; set; }
        public string nvBranchName { get; set; }
    }
}
