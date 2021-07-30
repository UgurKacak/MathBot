using AuthAPI.Entity;

namespace AuthAPI.Infrastructure
{
    public static class Extensions
    {
        public static UserDto AsDto(this User user)
        {
            return new UserDto(user.Id, user.UserName, user.Email, user.Password, user.DateOfBirth, user.FirstName, user.LastName, user.CreatedOn, user.ModifiedOn);
        }
    }
}
