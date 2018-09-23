namespace P02.UrlValidation
{
    using System;
    using System.Net;

    public class Program
    {
        public static void Main()
        {
            Url url = new Url();

            var encodedUrl = Console.ReadLine();
            string decodedUrl = WebUtility.UrlDecode(encodedUrl);

            bool success = url.ParseUrl(decodedUrl);

            if (!success)
            {
                Console.WriteLine("Invalid URL");
            }
            else
            {
                Console.WriteLine(url.ToString().TrimEnd());
            }

        }

    }
}
