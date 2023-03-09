using Entities;

namespace Service
{
    public interface ISurveysService
    {
        Task<List<Survey>> Get();
        Task<ResultsForSurvey> Get(int surveyId);
    }
}
