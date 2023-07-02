using Entities;
using Microsoft.AspNetCore.Mvc;

namespace Service
{
    public interface ISurveysService
    {
        Task<List<Survey>> GetByUserId(int permissionId, int? branchId = null);
        Task<ResultsForSurvey> Get(int surveyId, int permissionId, int? branchId = null);
    }
}
