using Entities;
using Microsoft.AspNetCore.Mvc;

namespace Service
{
    public interface ISurveysService
    {
        Task<List<Survey>> Get();
        Task<ResultsForSurvey> Get(int surveyId);
        IActionResult GetImage(string imageName);

    }
}
