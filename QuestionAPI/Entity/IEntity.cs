using System;
namespace QuestionAPI.Entity
{
    public interface IEntity
    {
        Guid Id { get; set; }
        DateTimeOffset CreatedOn { get; set; }
        DateTimeOffset ModifiedOn { get; set; }
    }
}
