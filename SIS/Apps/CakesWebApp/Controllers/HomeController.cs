namespace CakesWebApp.Controllers
{
    using SIS.HTTP.Enums;
    using SIS.HTTP.Responses;
    using SIS.WebServer.Results;

    public class HomeController : BaseController
    {
        public IHttpResponse Index()
        {
            return this.View("Index");
        }
    }
}
