using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Branch : IEntity
    {
        public int iId { get; set; }
        public string nvName { get; set; }
        public int iBranchId { get; set; }
        public string nvBranchName { get; set; }
    }
}
