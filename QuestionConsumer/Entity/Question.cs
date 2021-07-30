using System;
namespace QuestionConsumer.Entity
{
    public class Question : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Expression { get; set; }
        public string Result { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset ModifiedOn { get; set; }

    }
}
