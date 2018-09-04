namespace WebServer.Server.HTTP.Contracts
{
    using Server.Enums;

    public interface IHttpResponse
    {
        IHttpHeaderCollection Headers { get; }

        HttpStatusCode StatusCode { get; }

    }
}
