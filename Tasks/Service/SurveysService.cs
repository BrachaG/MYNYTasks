using Entities;
using Microsoft.Extensions.Configuration;
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
        IObjectGenerator<Question> _questionObjectGenerator;
        IObjectGenerator<Answer> _answerObjectGenerator;
        IObjectGenerator<Options> _optionsObjectGenerator;
        ILogger<SurveysService> _logger;
        IConfiguration _configuration;


        public SurveysService(ISqlDataAccess SqlDataAccess, IObjectGenerator<Survey> surveyObjectGenerator, ILogger<SurveysService> logger, IObjectGenerator<ResultsForSurvey> resultSurveyObjectGenerator, IObjectGenerator<ResultsForSurveyStudent> resultSurveyStudentObjectGenerator, IObjectGenerator<Question> questionObjectGenerator, IObjectGenerator<Answer> answerObjectGenerator, IObjectGenerator<Options> optionsObjectGenerator, IConfiguration configuration)
        {
            _SqlDataAccess = SqlDataAccess;
            _surveyObjectGenerator = surveyObjectGenerator;
            _logger = logger;
            _resultSurveyObjectGenerator = resultSurveyObjectGenerator;
            _resultSurveyStudentObjectGenerator = resultSurveyStudentObjectGenerator;
            _questionObjectGenerator = questionObjectGenerator;
            _answerObjectGenerator = answerObjectGenerator;
            _optionsObjectGenerator = optionsObjectGenerator;
            _configuration = configuration;
        }

        public async Task<List<Survey>> GetByUserId(int permissionId, int? branchId = null)
        {
            _logger.LogDebug("in Get all Surveys");
            try
            {
                List<SqlParameter> p = new List<SqlParameter> {
                new SqlParameter("iPermissionLevelId", permissionId),
                new SqlParameter("iBranchId", branchId)
            };
                DataTable dt = await _SqlDataAccess.ExecuteDatatableSP("su_GetSurveys_SLCT", p);
                List<Survey> surveys = _surveyObjectGenerator.GeneratListFromDataTable(dt);
                return surveys;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "in SurveyService, get all survey, faild when trying to approach to database");
            }
            return null;
        }

        public async Task<ResultsForSurvey> Get(int surveyId, int permissionId, int? branchId = null)
        {
            _logger.LogDebug("in Get Results For Survey");
            List<SqlParameter> p = new List<SqlParameter> {
                new SqlParameter("iSurveyId", surveyId) ,
                new SqlParameter("iPermissionLevelId", permissionId),
                new SqlParameter("iBranchId", branchId)
            };
            try
            {
                DataSet ds = await _SqlDataAccess.ExecuteDatasetSP("su_GetResultsForSurvey_SLCT", p);
                DataTable dtStudent = ds.Tables[0];
                DataTable dtQuestions = ds.Tables[1];
                DataTable dtAnswers = ds.Tables[2];
                DataTable dtOptions = ds.Tables[3];
                List<ResultsForSurveyStudent> students = _resultSurveyStudentObjectGenerator.GeneratListFromDataTable(dtStudent);
                List<Question> questions = _questionObjectGenerator.GeneratListFromDataTable(dtQuestions);
                List<Answer> answers = _answerObjectGenerator.GeneratListFromDataTable(dtAnswers);
                List<Options> options = _optionsObjectGenerator.GeneratListFromDataTable(dtOptions);
                foreach (Answer answer in answers)
                {
                    ResultsForSurveyStudent student = students.Find(s => s.iStudentId == answer.iStudentId);
                    if (student != null)
                    {
                        student.lAnswers.Add(answer);
                        student.image = _configuration["ImageUrl"] + student.iStudentId + ".png";
                    }
                }
                ResultsForSurvey results = new ResultsForSurvey();
                results.lResultsForSurveyStudent = students;
                results.lQuestions = questions;
                results.lOptions = options;
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

