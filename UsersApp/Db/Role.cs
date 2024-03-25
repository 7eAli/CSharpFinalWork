namespace UsersApp.Db
{
    public class Role
    {
        public RoleType RoleType { get; set; }
        public string Name { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
