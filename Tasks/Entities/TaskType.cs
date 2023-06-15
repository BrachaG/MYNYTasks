using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TaskType:IEntity
    {
        public int iTypeId { get; set; }
        public string nvTypeName { get; set; }    
    }
}
