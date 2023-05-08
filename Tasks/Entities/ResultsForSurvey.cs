namespace Entities
{
    public class ResultsForSurvey : IEntity
    {
        public List<ResultsForSurveyStudent> lResultsForSurveyStudent { get; set; }
        public List<Question> lQuestions { get; set; }
        public List<Options> lOptions { get; set; }
    }
}
