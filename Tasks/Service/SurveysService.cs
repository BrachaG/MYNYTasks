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
        IObjectGenerator<ResultsForSurvey> _resultSurveyObjectGenerator;
        IObjectGenerator<ResultsForSurveyStudent> _resultSurveyStudentObjectGenerator;
        ILogger<SurveysService> _logger;

        public SurveysService(ISqlDataAccess SqlDataAccess, IObjectGenerator<Survey> surveyObjectGenerator, ILogger<SurveysService> logger, IObjectGenerator<ResultsForSurvey> resultSurveyObjectGenerator, IObjectGenerator<ResultsForSurveyStudent> resultSurveyStudentObjectGenerator)
        {
            _SqlDataAccess = SqlDataAccess;
            _surveyObjectGenerator = surveyObjectGenerator;
            _logger = logger;
            _resultSurveyObjectGenerator = resultSurveyObjectGenerator;
            _resultSurveyStudentObjectGenerator = resultSurveyStudentObjectGenerator;
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

        public async Task<ResultsForSurvey> Get(int surveyId)
        {
            _logger.LogDebug("in Get Results For Survey");
            List<SqlParameter> p = new List<SqlParameter>    { new SqlParameter("iSurveyId",surveyId )};
                
            try
            {
                DataSet ds = await _SqlDataAccess.ExecuteDatasetSP("su_GetResultsForSurvey_SLCT", p);
                DataTable dtStudent = ds.Tables[0];
                DataTable dtQuestions = ds.Tables[1];
                DataTable dtAnswers = ds.Tables[2];
                List<ResultsForSurveyStudent> students = _resultSurveyStudentObjectGenerator.GeneratListFromDataTable(dtStudent);
                List<string> questions=new List<string>();
                foreach (DataRow question in dtQuestions.Rows)
                {
                    questions.Add(question[0].ToString());
                }
                foreach (DataRow answer in dtAnswers.Rows)
                {
                    ResultsForSurveyStudent student =students.Find(s => s.iStudentId == int.Parse(answer[0].ToString()));
                    if (student != null)
                    {
                        student.lAnswers.Add(answer[2].ToString());
                    }
                }
                ResultsForSurvey results = new ResultsForSurvey();
                results.lResultsForSurveyStudent = students;
                results.lQuestions = questions;
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "in SurveyService, get results for survey, faild when trying to approach to database");
                var b = ex.Message;
            }
            return null;
        }
    }
}
