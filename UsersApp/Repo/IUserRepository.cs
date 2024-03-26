using UsersApp.Db;
using UsersApp.Dto;

namespace UsersApp.Repo
{
    public interface IUserRepository
    {
        public string GetUserId(string email);
        public IEnumerable<UserDto> GetUsers();
        public string AddUser(UserDto userDto);
        public string DeleteUser(string email);
        public RoleType GetUserRole(string email, string password);
    }
}