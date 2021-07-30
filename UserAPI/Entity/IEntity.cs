using System;
namespace UserAPI.Entity
{
    public interface IEntity
    {
        Guid Id { get; set; }
        string UserName { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string DateOfBirth { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        DateTimeOffset CreatedOn { get; set; }
        DateTimeOffset ModifiedOn { get; set; }

    }
}
