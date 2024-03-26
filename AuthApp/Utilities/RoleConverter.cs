using AuthApp.Models.User;

namespace AuthApp.Utilities
{
    public static class RoleConverter
    {
        public static RoleType ConvertRole(RoleId roleId)
        {
            if (roleId == RoleId.Admin)
                return RoleType.Administrator;
            else
                return RoleType.User;
        }

        public static RoleId ConvertRole(RoleType roleId)
        {
            if (roleId == RoleType.Administrator)
                return RoleId.Admin;
            else
                return RoleId.User;
        }
    }
}
