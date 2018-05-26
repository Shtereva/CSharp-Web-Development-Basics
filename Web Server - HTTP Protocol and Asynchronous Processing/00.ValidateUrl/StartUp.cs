using System;
using System.Net;

namespace _00.ValidateUrl
{
    public class StartUp
    {
        public static void Main()
        {
            string inputUrl = Console.ReadLine();
            string decodedUrl = WebUtility.UrlDecode(inputUrl);

            var parsedUrl = new Uri(decodedUrl);

            string protocol = parsedUrl.Scheme;
            string host = parsedUrl.Host;
            int port = parsedUrl.Port;
            string path = parsedUrl.AbsolutePath;
            string query = parsedUrl.Query;
            string fragment = parsedUrl.Fragment;

            if (protocol == null || host == null || (protocol == "http" && port == 443) || (protocol == "https" && port == 80))
            {
                Console.WriteLine("Invalid Url");
                return;
            }

            Console.WriteLine($"Protocol: {protocol}");
            Console.WriteLine($"Host: {host}");
            Console.WriteLine($"Port: {port}");
            Console.WriteLine($"Path: {path}");

            if (!string.IsNullOrWhiteSpace(query))
            {
                Console.WriteLine($"Query: {query}");
            }

            if (!string.IsNullOrWhiteSpace(fragment))
            {
                Console.WriteLine($"Fragment: {fragment}");
            }
        }
    }
}