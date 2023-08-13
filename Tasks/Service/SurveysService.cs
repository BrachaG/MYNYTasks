using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository;
using System.Data;

namespace Service
{
    public class SurveysService : ISurveysService
    {
        ISqlDataAccess _sqlDataAccess;
        IObjectGenerator<Survey> _surveyObjectGenerator;
        ILogger<SurveysService> _logger;

        public SurveysService(ISqlDataAccess sqlDataAccess, IObjectGenerator<Survey> surveyObjectGenerator, ILogger<SurveysService> logger)
        {
            _sqlDataAccess = sqlDataAccess;
            _surveyObjectGenerator = surveyObjectGenerator;
            _logger = logger;
        }

        public async Task<ActionResult<List<Survey>>> Get()
        {
            _logger.LogDebug("in Get all Surveys");
            try
            {
                DataTable dt = await _sqlDataAccess.ExecuteDatatableSP("su_GetSurveys_SLCT", null);
                List<Survey> surveys = _surveyObjectGenerator.GeneratListFromDataTable(dt);
                return new ObjectResult(surveys) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "in SurveyService, get all survey, faild when trying to approach to database");
                return new ObjectResult(null) { StatusCode = 500 };
            }

        }
    }
}