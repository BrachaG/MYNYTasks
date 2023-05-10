using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Target : IEntity
    {
        public int iId { get; set; }
        public string nvComment { get; set; }
        public int iTargetId { get; set; }
        public int iPersonId { get; set; }
        public int iBranchId { get; set; }  
        public DateTime dtTargetDate { get; set; }
        public int iStatusId { get; set; }
    }
}
