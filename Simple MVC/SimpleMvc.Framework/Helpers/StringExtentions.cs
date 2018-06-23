using System.Globalization;

namespace SimpleMvc.Framework.Helpers
{
    public static class StringExtentions
    {
        public static string Capitalize(this string word)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(word);
        }
    }
}
