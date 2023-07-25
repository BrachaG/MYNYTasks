namespace Entities
{
    public class Answer : IEntity
    {
        public int iAnswerId { get; set; }
        public int iStudentId { get; set; }
        public int iQuestionId { get; set; }
        public string nvAnswer { get; set; }
    }
}
