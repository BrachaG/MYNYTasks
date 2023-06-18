using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BranchGroup:IEntity
    {
        public string nvGroupName { get; set; }
        public List<string> MyProperty { get; set; }
    }
}
