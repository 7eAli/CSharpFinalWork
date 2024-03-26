namespace AuthApp.Models.User
{
    public class UserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleId RoleId { get; set; }
    }
}
