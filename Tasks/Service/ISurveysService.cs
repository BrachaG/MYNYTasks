using Entities;

namespace Service
{
    public interface ISurveysService
    {
        Task<List<Survey>> Get();
        Task<Survey> Get(int surveyId);
    }
}
