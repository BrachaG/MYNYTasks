using Entities;
using Microsoft.AspNetCore.Mvc;

namespace Service
{
    public interface ISurveysService
    {
        Task<ActionResult<List<Survey>>> GetByUserId(int permissionId, int? branchId = null);
        Task<ActionResult<ResultsForSurvey>> Get(int surveyId, int permissionId, int? branchId = null);
    }
}
