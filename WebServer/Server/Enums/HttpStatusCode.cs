namespace WebServer.Server.Enums
{
    public enum HttpStatusCode
    {
        Ok = 200,
        MovedPermannetly = 301,
        Found = 302,
        SeeOther = 303,
        NotAuthorized = 401,
        NotFound = 404,
        InternalServerError = 500
    }
}
