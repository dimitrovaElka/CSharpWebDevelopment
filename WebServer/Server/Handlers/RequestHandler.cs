
namespace WebServer.Server.Handlers
{
    using System;
    using Contracts;
    using Server.HTTP.Contracts;
    using Server.HTTP;

    public abstract class RequestHandler : IRequestHandler
    {
        private readonly Func<IHttpRequest, IHttpResponse> handlingFunc;
        public RequestHandler(Func<IHttpRequest, IHttpResponse> handlingFunc)
        {
            CoreValidator.ThrowIfNull(handlingFunc, nameof(handlingFunc));
            this.handlingFunc = handlingFunc;
        }

        public IHttpResponse Handle(IHttpContext httpContext)
        {
            IHttpResponse response = this.handlingFunc(httpContext.Request);
            response.Headers.Add(new HttpHeader("Content-Type", "text/html"));

            return response;
        }
    }
}
