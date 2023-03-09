using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ResultsForSurvey
    {
        public List<ResultsForSurveyStudent> lResultsForSurveyStudent { get; set; }
        public List<string> lQuestions { get; set; }
    }
}
