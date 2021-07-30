using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.Infrastructure
{ 
    public record UserDto(Guid Id, string UserName, string Email, string Password, string DateOfBirth, string FirstName, string LastName, DateTimeOffset CreatedOn, DateTimeOffset ModifiedOn);

    public record CreateUserDto([Required] string UserName, [Required] string Email, [Required] string Password, [Required] string DateOfBirth, [Required] string FirstName, [Required] string LastName);

    public record UpdateUserDto([Required] string UserName, [Required] string Email, [Required] string Password, [Required] string DateOfBirth, [Required] string FirstName, [Required] string LastName);
}
