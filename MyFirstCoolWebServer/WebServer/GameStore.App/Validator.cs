using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace HTTPServer.GameStore.App
{
    public static class Validator
    {
        public static bool Email(string email)
        {
            return !NullOrEmpty(email)
                   && email.Contains("@")
                   && email.Contains(".")
                   && email.Length <= 30;
        }

        public static bool Password(string password, string confirmPassword)
        {
            return !NullOrEmpty(password)
                   && !NullOrEmpty(confirmPassword)
                   && password.Length >= 6
                   && password.Length <= 20
                   && password.Any(c => char.IsUpper(c) && char.IsLower(c) && char.IsDigit(c))
                   && password == confirmPassword;
        }

        public static bool FullName(string fullName)
        {
            return !NullOrEmpty(fullName)
                   && fullName.Length >= 2
                   && fullName.Length <= 20;
        }

        public static bool Title(string title)
        {
            return !NullOrEmpty(title)
                   && char.IsUpper(title[0])
                   && title.Length >= 3
                   && title.Length <= 100;
        }

        public static bool Price(string price)
        {
            return !NullOrEmpty(price) &&
                   decimal.Parse(price) >= 0
                   && Regex.Match(price.ToString(), @"\d+[.]?\d{0,2}").Success;
        }

        public static bool Size(string size)
        {
            return !NullOrEmpty(size) &&
                   double.Parse(size) >= 0
                   && Regex.Match(size.ToString(), @"\d+[.]?\d{0,1}").Success;
        }
        public static bool Trailer(string id)
        {
            return !NullOrEmpty(id)
                   && id.Length == 11;
        }

        public static bool Thumbnail(string image)
        {
            return !NullOrEmpty(image)
                   && (image.StartsWith(@"http://") || image.StartsWith(@"https://"));
        }

        public static bool Description(string description)
        {
            return !NullOrEmpty(description)
                   && description.Length >= 20;
        }

        public static bool Date(string date)
        {
            return !NullOrEmpty(date)
                   && Regex.Match(date, @"\d{4}-\d{2}-\d{2}").Success;
        }
        private static bool NullOrEmpty(string text)
        {
            return string.IsNullOrEmpty(text);
        }
    }
}
