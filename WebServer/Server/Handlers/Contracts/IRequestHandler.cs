
namespace WebServer.Server.Handlers.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using HTTP.Contracts;

    public interface IRequestHandler
    {
        IHttpResponse Handle(IHttpContext httpContext);
    }
}
