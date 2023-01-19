using Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class SurveysService : ISurveysService
    {
        ISqlDataAccess _SqlDataAccess;
        public SurveysService(ISqlDataAccess SqlDataAccess)
        {
            _SqlDataAccess = SqlDataAccess;
        }
        public async  Task<DataTable> GetSurveysByUserId()
        {

            try
            {
                DataTable surveys = await _SqlDataAccess.ExecuteDatatableSP("su_GetSurveys_SLCT",null);
               
            }
            catch (Exception ex)
            {
                var b = ex.Message;
            }
            return null;
        }
    }
}
