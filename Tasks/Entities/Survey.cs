namespace Entities
{
    public class Survey : IEntity

    {
        public int? iSurveyId { get; set; }
        public string nvSurveyName { get; set; }
        public DateTime? dtEndSurveyDate { get; set; }
        public string nvDescription { get; set; }
        public int? iReprisalType { get; set; }
        public int? iSumReprisal { get; set; }
        public string nvLink { get; set; }
        public int? iStatusType { get; set; }
        public string nvFinalMessage { get; set; }
        public int? iQuestionCount { get; set; }
        public int? iRespondentsCount { get; set; }
        public bool bFirstName { get; set; }
        public bool bLastName { get; set; }
        public bool bTz { get; set; }
        public bool bBranchId { get; set; }
        public bool bEmail { get; set; }
        public bool bPhone { get; set; }
        public bool bGender { get; set; }
    }
}
