using System;

namespace MyFirstCoolWebServer.Server.Exceptions
{
    public class InvalidResponseException : Exception
    {
        public InvalidResponseException(string message)
        : base(message)
        {
        }
    }
}