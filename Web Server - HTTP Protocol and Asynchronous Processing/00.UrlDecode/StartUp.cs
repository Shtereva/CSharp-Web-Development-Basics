using System;
using System.Net;

namespace _00.UrlDecode
{
    public class StartUp
    {
        public static void Main()
        {
            string inputUrl = Console.ReadLine();
            string decodedUrl = WebUtility.UrlDecode(inputUrl);

            Console.WriteLine(decodedUrl);
        }
    }
}