namespace RequestParser
{
    using System;
    using System.Text;
    using System.Collections.Generic;

    public static class Program
    {
        public static void Main()
        {
            Dictionary<string, List<string>> requests = new Dictionary<string, List<string>>();

            string input;
            while ((input = Console.ReadLine()).ToLower() != "end")
            {
                var index = input.LastIndexOf('/');
                var path = input.Substring(0, index);
                var method = input.Substring(index + 1);
                if (!requests.ContainsKey(path))
                {
                    requests[path] = new List<string>();
                }
                requests[path].Add(method);
            }

            input = Console.ReadLine();
            var parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string searchedMethod = parts[0].ToLower();
            string searchedPath = parts[1].ToLower();
            string response;

            if (!requests.ContainsKey(searchedPath) || !requests[searchedPath].Contains(searchedMethod))
            {
                response = "404 NotFound";
            }
            else
            {
                response = "200 OK";
            }

            var myResponse = new MyResponse(response);

            Console.WriteLine(myResponse.ToString().Trim());
        }
    }
}
