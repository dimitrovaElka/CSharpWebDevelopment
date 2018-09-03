﻿
namespace WebServer.Application.Controllers
{
    using Application.Views.Home;
    using Server.Enums;
    using Server.HTTP.Contracts;
    using Server.HTTP.Response;

    public class HomeController
    {
        // GET /
        public IHttpResponse Index()
        {
            return new ViewResponse(HttpStatusCode.Ok, new IndexView());
        }
    }
}