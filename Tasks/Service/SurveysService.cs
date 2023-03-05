using Entities;

using Microsoft.Extensions.Logging;
using Repository;
using System.Data;
using System.Data.SqlClient;

namespace Service
{
    public class SurveysService : ISurveysService
    {
        ISqlDataAccess _SqlDataAccess;
        IObjectGenerator<Survey> _surveyObjectGenerator;
        ILogger<SurveysService> _logger;

        public SurveysService(ISqlDataAccess SqlDataAccess, IObjectGenerator<Survey> surveyObjectGenerator, ILogger<SurveysService> logger)
        {
            _SqlDataAccess = SqlDataAccess;
            _surveyObjectGenerator = surveyObjectGenerator;
            _logger = logger;
        }

        public async Task<List<Survey>> Get()
        {
            _logger.LogDebug("in Get all Surveys");
            try
            {
                DataTable dt = await _SqlDataAccess.ExecuteDatatableSP("su_GetSurveys_SLCT", null);
                List<Survey> surveys = _surveyObjectGenerator.GeneratListFromDataTable(dt);
                return surveys;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "in SurveyService, get all survey, faild when trying to approach to database");
                var b = ex.Message;
            }
            return null;
        }

        public async Task<Survey> Get(int surveyId)
        {
            _logger.LogDebug("in Get one Survey");
            List<SqlParameter> p = new List<SqlParameter>    { new SqlParameter("iSurveyId",surveyId )};
                
      /*      try
            {
                DataSet dt = await _SqlDataAccess.ExecuteDatasetSP("su_GetResultsForSurvey_SLCT", p);
                Survey survey = _surveyObjectGenerator.GeneratListFromDataSet(dt);
                return survey;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "in SurveyService, get all survey, faild when trying to approach to database");
                var b = ex.Message;
            }*/
            return null;
        }
    }
}
