using System.Text.RegularExpressions;

namespace AuthApp.Utilities
{
    public static class EmailCheck
    {
        public static bool CheckEmail(string email)
        {
            Regex validFormat = new Regex("^\\S+@\\S+\\.\\S+$");
            return validFormat.IsMatch(email);
        }
    }
}
