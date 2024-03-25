using UsersApp.Db;
using UsersApp.Dto;

namespace UsersApp.Repo
{
    public interface IUserRepository
    {
        public Guid GetUserId(string email);
        public void AddUser(UserDto userDto);
        public RoleType GetUserRole(string email, string password);
    }
}