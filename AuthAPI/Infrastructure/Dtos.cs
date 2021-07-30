using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAPI.Infrastructure
{ 
    public record UserDto(Guid Id, string UserName, string Email, string Password, string DateOfBirth, string FirstName, string LastName, DateTimeOffset CreatedOn, DateTimeOffset ModifiedOn);

    public record LoginUserDto([Required] string UserName, [Required] string Password);
}
