using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User
    {
        public int iPersonId { get; set; }
        public string nvUserName { get; set; }
        public string nvPasswordperty { get; set; }
        public string nvPasswordHint { get; set; }
        public int iLastUpdateUserId { get; set; }
        public DateTime dtLastUpdateDate { get; set; }
        public int iSysRowStatus { get; set; }
        public int iPersonStatusId { get; set; }
        public int iAdvancedOptionsId { get; set; }
        public int iValidUserNameStatus { get; set; }
      
    }
}
