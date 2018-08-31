namespace WebServer.Server.HTTP.Contracts
{
    using Server.Enums;

    public interface IHttpResponse
    {
        HttpHeaderCollection Headers { get; }

        HttpStatusCode StatusCode { get; }

      //  string StatusMessage { get; }
    }
}
