
namespace CakesWebApp.Controllers
{
    using CakesWebApp.Models;
    using CakesWebApp.Services;
    using SIS.HTTP.Cookies;
    using SIS.HTTP.Enums;
    using SIS.HTTP.Requests;
    using SIS.HTTP.Responses;
    using SIS.WebServer.Results;
    using System;
    using System.Linq;

    public class AccountController : BaseController
    {
        private IHashService hashService;

        public AccountController()
        {
            this.hashService = new HashService();
        }

        public IHttpResponse Register(IHttpRequest request)
        {
            return this.View("Register");
        }

        public IHttpResponse DoRegister(IHttpRequest request)
        {
            var userName = request.FormData["username"].ToString().Trim();
            var password = request.FormData["password"].ToString();
            var confirmPassword = request.FormData["confirmpassword"].ToString();
            // validate
            if (string.IsNullOrEmpty(userName) || userName.Length < 4)
            {
                this.BadRequestError("Please provide valid username with length of 4 or more characters.");
            }
            if (this.Db.Users.Any(x => x.Username == userName))
            {
                return this.BadRequestError("User with the same name already exists.");
            }

            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            {
                return this.BadRequestError("Please provide password of length 6 or more.");
            }

            if (password != confirmPassword)
            {
                return this.BadRequestError("Passwords do not match.");
            }

            // Hash password
            var hashedPassword = this.hashService.Hash(password);

            // Create user
            var user = new User
            {
                Name = userName,
                Username = userName,
                Password = hashedPassword,
            };
            this.Db.Users.Add(user);

            try
            {
                this.Db.SaveChanges();
            }
            catch (Exception e)
            {
                // TODO: Log error
                return this.ServerError(e.Message);
            }

            // TODO: Login

            // Redirect
            return new RedirectResult("/");
            // return new HtmlResult("Registered", HttpResponseStatusCode.Ok);
        }

        public IHttpResponse Login(IHttpRequest request)
        {
            return this.View("Login");
        }

        public IHttpResponse DoLogin(IHttpRequest request)
        {
            var userName = request.FormData["username"].ToString().Trim();
            var password = request.FormData["password"].ToString();

            var hashedPassword = this.hashService.Hash(password);

            var user = this.Db.Users.FirstOrDefault(x =>
                x.Username == userName &&
                x.Password == hashedPassword);

            if (user == null)
            {
                return this.BadRequestError("Invalid username or password.");
            }

            var cookieContent = this.UserCookieService.GetUserCookie(user.Username);

            var response = new RedirectResult("/");
            var cookie = new HttpCookie(".auth-cakes", cookieContent, 7) { HttpOnly = true };
            response.Cookies.Add(cookie);
            return response;
        }
    }
}
