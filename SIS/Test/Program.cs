
namespace Test
{
    using SIS.HTTP.Enums;
    using SIS.HTTP.Extensions;
    using SIS.HTTP.Requests;
    using System;
    class Program
    {
        public static void Main(string[] args)
        {

            //string s = "the Very quick Brown Fox jumped 3 TIMES over the lazy dog.";

            //HttpResponseStatusCode responseCode = HttpResponseStatusCode.Ok;

            //string res = s.Capitalize();

            //string codeAsString = responseCode.GetResponseLine();

            //Console.WriteLine(codeAsString);

            //string inputTestGet = $@"GET /home/index?search=nissan&category=SUV#hashtag HTTP/1.1 {Environment.NewLine}Host: localhost:8000 {Environment.NewLine}Accept: text/plain {Environment.NewLine}Cashe-Control: no-cashe {Environment.NewLine}{Environment.NewLine}";
            string inputTestGet = $@"GET /home/index?search=nissan&category=SUV HTTP/1.1 {Environment.NewLine}Host: localhost:8000 {Environment.NewLine}Accept: text/plain {Environment.NewLine}Cashe-Control: no-cashe {Environment.NewLine}{Environment.NewLine}";
            HttpRequest request = new HttpRequest(inputTestGet);

            var path = request.Path;

            var url = request.Url;
            var headers = request.Headers;
            var method = request.RequestMethod;

            HttpResponseStatusCode statusCode = HttpResponseStatusCode.Forbidden;

            Console.WriteLine(statusCode.GetResponseLine());

            Console.WriteLine(request);
        }
    }
}
