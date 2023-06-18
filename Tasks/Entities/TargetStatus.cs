using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TargetStatus: IEntity
    {
        public int iTargetStatusId { get; set; }
        public string nvTargetStatusName { get; set; }
    }
}
