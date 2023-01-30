using Entities;
using Repository;
using System.Data;

namespace Service
{
    public class SurveysService : ISurveysService
    {
        ISqlDataAccess _SqlDataAccess;
        IObjectGenerator<Survey> _surveyObjectGenerator;
       
        public SurveysService(ISqlDataAccess SqlDataAccess, IObjectGenerator<Survey> surveyObjectGenerator)
        {
            _SqlDataAccess = SqlDataAccess;
            _surveyObjectGenerator = surveyObjectGenerator;   
        }

        public async Task<List<Survey>> Get()
        {
            try
            {
                DataTable dt = await _SqlDataAccess.ExecuteDatatableSP("su_GetSurveys_SLCT", null);
                List<Survey> surveys = _surveyObjectGenerator.GeneratListFromDataTable(dt);
                return surveys;
            }
            catch (Exception ex)
            {
                var b = ex.Message;
            }
            return null;
        }
    }
}
