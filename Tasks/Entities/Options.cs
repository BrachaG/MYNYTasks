using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Options
    {
        public int iAnswerId { get; set; }
        public int iQuestionId { get; set; }
        public string nvAnswerName { get; set; }
        public int sum { get; set; }
        public string[] responders { get; set; } 
    }
}
