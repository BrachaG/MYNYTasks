using Entities;
using Microsoft.AspNetCore.Mvc;
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

        public SurveysService(ISqlDataAccess SqlDataAccess, IObjectGenerator<Survey> surveyObjectGenerator, ILogger<SurveysService> logger, IObjectGenerator<ResultsForSurvey> resultSurveyObjectGenerator, IObjectGenerator<ResultsForSurveyStudent> resultSurveyStudentObjectGenerator, IObjectGenerator<Question> questionObjectGenerator, IObjectGenerator<Answer> answerObjectGenerator, IObjectGenerator<Options> optionsObjectGenerator)
        {
            _SqlDataAccess = SqlDataAccess;
            _surveyObjectGenerator = surveyObjectGenerator;
            _logger = logger;
            _resultSurveyObjectGenerator = resultSurveyObjectGenerator;
            _resultSurveyStudentObjectGenerator = resultSurveyStudentObjectGenerator;
            _questionObjectGenerator = questionObjectGenerator;
            _answerObjectGenerator = answerObjectGenerator;
            _optionsObjectGenerator = optionsObjectGenerator;
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
            List<SqlParameter> p = new List<SqlParameter> { new SqlParameter("iSurveyId", surveyId) };

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
                        FileContentResult f = (FileContentResult)GetImage(student.iStudentId.ToString() + ".png");
                        student.image = f.FileContents;
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
        public IActionResult GetImage(string imageName)
        {
            // Get the full path of the image file
            string imagePath = Path.Combine("C:\\Users\\יעלי\\OneDrive\\מסמכים\\My Documents\\NY\\MYNYTasks\\Tasks\\images", imageName);

            // Check if the image file exists
            if (!File.Exists(imagePath))
            {
                // Return a 404 Not Found response if the image doesn't exist
                return new StatusCodeResult(404);
            }

            // Read the image file data
            byte[] imageData = File.ReadAllBytes(imagePath);

            // Determine the content type based on the image file extension
            string contentType = GetContentType(imageName);

            // Return the image data as a file result with the appropriate content type
            return new FileContentResult(imageData, contentType);
        }

        private string GetContentType(string fileName)
        {
            // Get the file extension
            string fileExtension = Path.GetExtension(fileName)?.ToLowerInvariant();

            // Set the content type based on the file extension
            switch (fileExtension)
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".gif":
                    return "image/gif";
                case ".bmp":
                    return "image/bmp";
                default:
                    // If the file extension is not recognized, you can set a default content type
                    return "application/octet-stream";
            }
        }

     
    }

}

