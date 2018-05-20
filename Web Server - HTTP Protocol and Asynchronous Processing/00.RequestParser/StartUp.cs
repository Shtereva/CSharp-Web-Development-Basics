using System;
using System.Collections.Generic;

namespace _00.RequestParser
{
    public class StartUp
    {
        public static void Main()
        {
            string input;
            var pathMethods = new Dictionary<string, HashSet<string>>();

            while ((input = Console.ReadLine()) != "END")
            {
                var urlParts = input.Split("/", StringSplitOptions.RemoveEmptyEntries);

                string path = $"/{urlParts[0]}";
                string method = urlParts[1];

                if (!pathMethods.ContainsKey(path))
                {
                    pathMethods[path] = new HashSet<string>();
                }

                pathMethods[path].Add(method);
            }

            var urlResquestInput = Console.ReadLine().Split();

            string requestMethod = urlResquestInput[0].ToLower();
            string requestPath = urlResquestInput[1];
            string status = urlResquestInput[2];

            string statusCode = "200";
            string statusText = "OK";

            if (!pathMethods.ContainsKey(requestPath) || !pathMethods[requestPath].Contains(requestMethod))
            {
                statusCode = "404";
                statusText = "Not Found";
            }

            Console.WriteLine($"{status} {statusCode} {statusText}");
            Console.WriteLine($"Content-Length: {statusText.Length}");
            Console.WriteLine("Content-Type: text/plain" + Environment.NewLine);
            Console.WriteLine(statusText);
        }
    }
}
