namespace Entities
{
    public class Answer : IEntity
    {
        public int iStudentId { get; set; }
        public int iQuestionId { get; set; }
        public string nvAnswer { get; set; }
    }
}
