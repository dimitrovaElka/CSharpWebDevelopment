
namespace WebServer.Server.Handlers
{
    using Common;
    using Contracts;
    using Server.HTTP.Contracts;
    using Server.HTTP;
    using System;

    public class RequestHandler : IRequestHandler
    {
        private readonly Func<IHttpRequest, IHttpResponse> handlingFunc;
        public RequestHandler(Func<IHttpRequest, IHttpResponse> handlingFunc)
        {
            CoreValidator.ThrowIfNull(handlingFunc, nameof(handlingFunc));
            this.handlingFunc = handlingFunc;
        }

        public IHttpResponse Handle(IHttpContext context)
        {
            IHttpResponse response = this.handlingFunc(context.Request);
            // response.Headers.Add(new HttpHeader("Content-Type", "text/html"));
            if (!response.Headers.ContainsKey(HttpHeader.ContentType))
            {
                response.Headers.Add(HttpHeader.ContentType, "text/html");
            }

            return response;
        }
    }
}
