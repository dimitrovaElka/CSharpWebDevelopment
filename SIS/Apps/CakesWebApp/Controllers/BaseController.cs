namespace CakesWebApp.Controllers
{
    using CakesWebApp.Data;
    using CakesWebApp.Services;
    using SIS.HTTP.Enums;
    using SIS.HTTP.Requests;
    using SIS.HTTP.Responses;
    using SIS.WebServer.Results;
    using System.IO;

    public abstract class BaseController
    {
        public BaseController()
        {
            this.UserCookieService = new UserCookieService();

            this.Db = new CakesDbContext();
        }

        protected CakesDbContext Db { get; }

        protected UserCookieService UserCookieService { get; }

        protected string GetUsername(IHttpRequest request)
        {
            if (!request.Cookies.ContainsCookie(".auth-cakes"))
            {
                return null;
            }

            var cookie = request.Cookies.GetCookie(".auth-cakes");
            var cookieContent = cookie.Value;
            var userName = this.UserCookieService.GetUserData(cookieContent);
            return userName;

        }

        protected IHttpResponse View(string viewName)
        {
            var content = File.ReadAllText("Views/" + viewName + ".html");

            return new HtmlResult(content, HttpResponseStatusCode.Ok);
        }

        protected IHttpResponse BadRequestError(string errorMessage) => new HtmlResult($"<h1>{errorMessage}</h1>", HttpResponseStatusCode.BadRequest);

        protected IHttpResponse ServerError(string errorMessage) => new HtmlResult($"<h1>{errorMessage}</h1>", HttpResponseStatusCode.InternalServerError);
    }
}
