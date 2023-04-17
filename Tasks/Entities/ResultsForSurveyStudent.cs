namespace Entities
{
    public class ResultsForSurveyStudent : IEntity
    {
        public int iStudentId { get; set; } //הוספנו
        public DateTime? dtCreateDate { get; set; }
        public string nvFullName { get; set; }
        public string nvGender { get; set; }
        public string nvBranchName { get; set; }
        public string nvMobileNumber { get; set; }
        public string nvEmail { get; set; }
        public string nvProgramName { get; set; }
        public List<Answer> lAnswers { get; set; } = new List<Answer>(); //הוספנו אתחול
    }
}