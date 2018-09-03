
namespace WebServer.Server.HTTP.Response
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Contracts;
    using Enums;

    public abstract class HttpResponse : IHttpResponse
    {
        private string StatusMessage => this.StatusCode.ToString();

        protected HttpResponse()
        {
            this.Headers = new HttpHeaderCollection();
        }

        //protected HttpResponse(string redirectUrl) :this()
        //{
        //}

        //protected HttpResponse(HttpStatusCode responseCode, IView view) : this()
        //{
        //}

        public HttpHeaderCollection Headers { get; }

        public HttpStatusCode StatusCode { get; protected set; }

        public override string ToString()
        {
            StringBuilder response = new StringBuilder();
            var statusCodeNumber = (int)this.StatusCode;
            response.AppendLine($"HTTP/1.1 {statusCodeNumber} {this.StatusMessage}");
            response.AppendLine(this.Headers.ToString());
            
            return response.ToString();
        }
    }
}
