namespace P01.UrlDecoder
{
    using System;
    using System.Net;

    public class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            string decodedUrl = WebUtility.UrlDecode(input);
            Console.WriteLine(decodedUrl);
        }
    }
}
