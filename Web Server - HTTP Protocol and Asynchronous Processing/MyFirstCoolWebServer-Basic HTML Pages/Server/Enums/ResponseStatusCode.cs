﻿namespace MyFirstCoolWebServer.Server.Enums
{
    public enum ResponseStatusCode
    {
        Ok = 200,
        MovedPermanently = 301,
        Found = 302,
        MovedTemporarily = 303,
        NotAuthorized = 401,
        NotFound = 404,
        InternalServerError = 500
    }
}