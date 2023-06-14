using Entities;

namespace Service
{
    public interface ISurveysService
    {
        Task<List<Survey>> GetByUserId(string userId, string permissionId);
        Task<ResultsForSurvey> Get(int surveyId);
    }
}
