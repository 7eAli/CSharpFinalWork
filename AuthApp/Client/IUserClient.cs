using AuthApp.Models.User;

namespace AuthApp.Client
{
    public interface IUserClient
    {
        public Task<Guid> DeleteUser(string email);
        
        public Task<IEnumerable<UserDto>> GetUsers();
    }
}
