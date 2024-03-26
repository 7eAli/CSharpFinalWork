using AuthApp.Models.Authorization;
using AuthApp.Models.User;

namespace AuthApp.Client
{
    public interface ILoginClient
    {
        public Task<Guid> RegisterAdmin(UserDto admin);
        public Task<Guid> RegisterUser(UserDto user);
        public Task<RoleType> GetUserRole(LoginModel loginModel);
        public Task<Guid> GetUserId(string email);
    }
}
