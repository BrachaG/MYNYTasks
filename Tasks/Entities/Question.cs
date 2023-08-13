namespace Entities
{
    public class Question : IEntity
    {
        public int iQuestionId { get; set; }
        public string nvQuestionText { get; set; }
        public string nvQuestionTypeName { get; set; }
    }
}
