using System;

namespace MyFirstCoolWebServer.Server.Exceptions
{
    public class BadRequestExeption : Exception
    {
        public BadRequestExeption(string message)
        : base(message)
        {
        }
    }
}