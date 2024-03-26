namespace AuthApp.Utilities
{
    public static class PasswordCheck
    {
        public static bool CheckPassword(string password)
        {
            bool result = false;
            result = ContainsLower(password);
            result = ContainsNumber(password);
            result = ContainsUpper(password);
            result = ContainsPunctuation(password);
            result = LengthCheck(password);
            return result;
        }

        private static bool ContainsLower(string password)
        {
            foreach (char c in password)
            {
                if (char.IsLetter(c) && char.IsLower(c))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool ContainsUpper(string password)
        {
            foreach (char c in password)
            {
                if (char.IsLetter(c) && char.IsUpper(c))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool ContainsNumber(string password)
        {
            foreach (char c in password)
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool ContainsPunctuation(string password)
        {
            foreach (char c in password)
            {
                if (char.IsPunctuation(c))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool LengthCheck(string password)
        {
            if (password.Length < 10)
                return false;
            return true;
        }
    }
}
