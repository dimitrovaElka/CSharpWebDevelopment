
namespace WebServer.Server.HTTP.Response
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Enums;

    public class NotFoundResponse : HttpResponse
    {
        public NotFoundResponse()
        {
            this.StatusCode = HttpStatusCode.NotFound;
        }
    }
}
