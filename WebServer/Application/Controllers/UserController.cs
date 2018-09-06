namespace WebServer.Application.Controllers
{   
    using Server.Enums;
    using Server.HTTP.Contracts;
    using Server.HTTP.Response;
    using Views;

    public class UserController
    {
        public IHttpResponse RegisterGet()
        {
            return new ViewResponse(HttpStatusCode.Ok, new RegisterView());
        }
    }
}
