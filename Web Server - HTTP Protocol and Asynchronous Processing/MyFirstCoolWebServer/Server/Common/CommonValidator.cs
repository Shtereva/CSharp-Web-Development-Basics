using System;

namespace MyFirstCoolWebServer.Server.Common
{
    public static class CommonValidator
    {
        public static void NullCheck(object obj, string name)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void NullOrWhiteSpaceCheck(string text, string name)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException($"{name} cannot be null or empty.");
            }
        }
    }
}
