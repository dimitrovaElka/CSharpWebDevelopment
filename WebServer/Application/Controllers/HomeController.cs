
namespace WebServer.Application.Controllers
{
    using System;
    using System.Collections.Generic;
    using Application.Views;
    using Server.HTTP.Contracts;
    using Server.Enums;
    using Server.HTTP.Response;

    public class HomeController
    {
        public IHttpResponse Index()
        {
            return new ViewResponse(HttpStatusCode.Ok, new HomeIndexView());
        }
    }
}
