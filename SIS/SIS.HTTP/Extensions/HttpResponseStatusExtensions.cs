
namespace SIS.HTTP.Extensions
{
    using System;
    using Enums;

    public static class HttpResponseStatusExtensions 
    {
        public static string GetResponseLine(this HttpResponseStatusCode code)
        {
            //int statusCodeAsNumber = (int)code;
            //string result = statusCodeAsNumber + " " + code.ToString();
            //return result;

            return $"{(int)code} {code}";
        }
    }
}
