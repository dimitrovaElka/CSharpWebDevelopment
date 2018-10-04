namespace SIS.WebServer.Results
{
    using SIS.HTTP.Enums;
    using SIS.HTTP.Headers;
    using SIS.HTTP.Responses;
    using System.Text;

    public class RedirectResult : HttpResponse
    {
        public RedirectResult(string location)
            : base(HttpResponseStatusCode.SeeOther)
        {
            this.Headers.Add(new HttpHeader("Location", location));
        }
    }
}
