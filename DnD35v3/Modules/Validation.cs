using System.Text.RegularExpressions;

namespace DnD35v3.Modules
{
    public class Validation
    {
        public bool IsValidUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return false;
            if (username.Length < 3 || username.Length > 20) return false;

            Regex regex = new Regex("^[a-zA-Z0-9 ]*$");
            return regex.IsMatch(username);
        }

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return true;

            // Regex pattern for typical email validation
            string pattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
    }
}