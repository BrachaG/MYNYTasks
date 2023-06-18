using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TargetType: IEntity
    {
        public int itypeTargetId { get; set; }
        public string nvTargetName { get; set; }
    }
}
