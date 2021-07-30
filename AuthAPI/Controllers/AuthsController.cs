using AuthAPI.Business;
using AuthAPI.Entity;
using AuthAPI.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;


namespace AuthAPI.Controllers
{
    [ApiController]
    [Route("api/auths")]
    public class AuthsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<User> repository;
        public AuthsController(IConfiguration configuration, IRepository<User> repository)
        {
            _configuration = configuration;
            this.repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> PostAsync(LoginUserDto loginUserDto)
        {

            if (loginUserDto.UserName == null) {
                return BadRequest("username required!");
            } 
            
            if (loginUserDto.Password == null) {
                return BadRequest("password required!");
            }

            var existingUser = await repository.GetByUserNameAsync(loginUserDto.UserName);

            if (existingUser == null)
            {
                return StatusCode(401, "username or password wrong!");
            }

            if (!String.Equals(loginUserDto.Password, existingUser.Password))
            {
                return StatusCode(401, "username or password wrong!");
            }

            TokenHandler._configuration = _configuration;
            return Ok(TokenHandler.CreateAccessToken(existingUser.Id));

        }

    }

}