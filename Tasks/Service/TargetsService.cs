using Entities;
using Microsoft.Extensions.Logging;
using Repository;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;


namespace Service
{
    public class TargetsService : ITargetsService
    {
        ISqlDataAccess _SqlDataAccess;
        ILogger<TargetsService> _logger;
        IObjectGenerator<Target> _targetObjectGenerator;

        public TargetsService(ISqlDataAccess SqlDataAccess, ILogger<TargetsService> logger, IObjectGenerator<Target> userObjectGenerator)
        {
            _SqlDataAccess = SqlDataAccess;
            _logger = logger;
            _targetObjectGenerator = userObjectGenerator;
        }
        public async Task<List<Target>> GetTargetsByUserId(int userId, int permissionLevelId)
        {
            _logger.LogDebug("GetTargetsByUserId", userId, permissionLevelId);
            List<SqlParameter> parameters = new List<SqlParameter> {
              { new SqlParameter("id",userId )},
              { new SqlParameter("PermissionLevelId", permissionLevelId)}
             };
            try
            {
                DataTable targets = await _SqlDataAccess.ExecuteDatatableSP("su_Get_Targets", parameters);
                if (targets.Rows.Count > 0)
                {
                    List<Target> t = _targetObjectGenerator.GeneratListFromDataTable(targets);
                    return t;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("GetTargetsByUserId ", ex);
            }
            return null;
        }

        public async Task AddTarget(string comment, int typeTargetId, int[] personId, DateTime? targetDate, int creatorId)
        {
            DataTable personIds = new DataTable();
            personIds.Columns.Add("Id", typeof(int));

            // Populate the DataTable with the list of PersonId values
            foreach (int id in personId)
            {
                personIds.Rows.Add(id);
            }

            List<SqlParameter> parameters = new List<SqlParameter> {
            new SqlParameter("Comment", comment),
            new SqlParameter("typeTargetId", typeTargetId),
            new SqlParameter
            {
            ParameterName = "Ids",
            SqlDbType = SqlDbType.Structured,
            TypeName = "dbo.PersonIds",
            Value = personIds
            },
            new SqlParameter("TargetDate", targetDate)
            ,
            new SqlParameter("CreatorId", creatorId)};

            try
            {
                await _SqlDataAccess.ExecuteDatatableSP("su_Insert_Target", parameters);
                List<SqlParameter> p = new List<SqlParameter> {
                    new SqlParameter
                    {
                    ParameterName = "Ids",
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "dbo.PersonIds",
                    Value = personIds
                    }
                };
               DataTable dt = await _SqlDataAccess.ExecuteDatatableSP("su_GetUserEmails_SLCT", p);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to insert target", ex);
            }
        }
        public void SendEmail(string recipient, string subject, string body)
        {
            var message = new MailMessage();
            message.From = new MailAddress("36214085573@mby.co.il", "Nefesh Yehudi");
            message.To.Add(new MailAddress(recipient));
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = false;

            using (var client = new SmtpClient("smtp.office365.com", 587))
            {
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential("36214085573@mby.co.il", "Student@264");
                client.Send(message);
            }
        }

    }
}