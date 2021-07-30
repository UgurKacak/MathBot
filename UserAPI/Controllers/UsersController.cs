using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.Entity;
using UserAPI.Infrastructure;

namespace UserAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User> repository;
        public UsersController(IRepository<User> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAsync()
        {
            var users = (await repository.GetAllAsync()).Select(a => a.AsDto());
          
            return Ok(users);
        }
       
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetByIdAsync(Guid id)
        {
            var user = await repository.GetAsync(id);
            if (user == null)
            {
                NotFound();
            }

            return user.AsDto();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserDto>> PostAsync(CreateUserDto createUserDto)
        {
            var existingEmail = await repository.GetByEmailAsync(createUserDto.Email);
            if (existingEmail != null)
            {
                return BadRequest(createUserDto.Email + " is already exist.");
            }

            var existingUserName = await repository.GetByUserNameAsync(createUserDto.UserName);
            if (existingUserName != null)
            {
                return BadRequest(createUserDto.UserName + " is already exist.");
            }

            var user = new User
            {
                UserName = createUserDto.UserName,
                Email = createUserDto.Email,
                Password = createUserDto.Password,
                DateOfBirth = createUserDto.DateOfBirth,
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                CreatedOn = DateTimeOffset.UtcNow
            };

            await repository.CreateAsync(user);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateUserDto updateUserDto)
        {
            var existingUser = await repository.GetAsync(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.UserName = updateUserDto.UserName;
            existingUser.Email = updateUserDto.Email;
            existingUser.Password = updateUserDto.Password;
            existingUser.DateOfBirth = updateUserDto.DateOfBirth;
            existingUser.FirstName = updateUserDto.FirstName;
            existingUser.LastName = updateUserDto.LastName;
            existingUser.ModifiedOn = DateTimeOffset.UtcNow;

            await repository.UpdateAsync(existingUser);
            return Ok(existingUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var user = await repository.GetAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            await repository.RemoveAsync(user.Id);

            return NoContent();
        }
    }
}
