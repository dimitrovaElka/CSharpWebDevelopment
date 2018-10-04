namespace RequestParser
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class MyResponse
    {
        public MyResponse(string response)
        {
            var index = response.IndexOf(' ');

            this.StatusCode = response.Substring(0, index);
            this.ResponseText = response.Substring(index + 1);
        }
        public string StatusCode { get; private set; }

        public string ResponseText { get; private set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"HTTP/1.1 {this.StatusCode} {this.ResponseText}");
            builder.AppendLine($"Content-Length: {this.ResponseText.Length}");
            builder.AppendLine("Content-Type: text/plain");
            builder.AppendLine();
            builder.AppendLine(this.ResponseText);

            return builder.ToString();
        }
    }
}
