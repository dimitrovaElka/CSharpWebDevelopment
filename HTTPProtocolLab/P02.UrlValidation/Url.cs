using System;
using System.Text;
using System.Text.RegularExpressions;

namespace P02.UrlValidation
{
    public class Url
    {
        public Url()
        {
        }

        
        public string Protokol { get; private set; }
        public string Host { get; private set; }
        public int Port { get; private set; }

        public string Path { get; private set; } = "/";

        public string Query { get; private set; }
        public string Fragment { get; private set; }

        public bool ParseUrl(string url)
        {
            string pattern = @"^(http|https|ftp)\://([a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3})(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\/]*)([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~]*[^\.\,\)\(\{\}\s]?)$";

            Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            if (!reg.IsMatch(url))
            {
                return false;
            }

            var match = reg.Match(url);
            this.Protokol = match.Groups[1].Value.ToLower();
            this.Host = match.Groups[2].Value;
            ////int indexOfQuery = 0;
            ////if (match.Groups.Count == 4)
            ////{
            ////    indexOfQuery = match.Groups[3].Index + match.Groups[3].Value.Length;
            ////}
            ////else
            ////{
            ////    indexOfQuery = match.Groups[2].Index + match.Groups[2].Value.Length; ;
            ////}
            int defaultPort = SetDefaultPort();
            if (match.Groups[3].Value != "" && match.Groups[3].Value.Substring(1) != defaultPort.ToString())
            {
                return false;
            }
            this.Port = defaultPort;
            if (match.Groups[4].Value != "")
            {
                this.Path = "/" + match.Groups[4].Value;
            }
            if (match.Groups[5].Value != "")
            {
                ParseQueryAndFragment(match.Groups[5].Value);
            }
            return true;
        }

        private void ParseQueryAndFragment(string value)
        {
            string queryAndFragment = value.Substring(1);
            var parts = queryAndFragment.Split('#');
            this.Query = parts[0];
            if (parts.Length == 2)
            {
                this.Fragment = parts[1];
            }
        }

        private int SetDefaultPort()
        {
            int defaultPort;
            switch (this.Protokol)
            {
                case "http":
                    defaultPort = 80;
                    break;
                case "ftp":
                    defaultPort = 20;
                    break;
                case "https":
                    defaultPort = 443;
                    break;
                default:
                    defaultPort = -1;
                    break;
            }
            return defaultPort;
        }

   

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"Protokol: {this.Protokol}");
            builder.AppendLine($"Host: {this.Host}");
            builder.AppendLine($"Port: {this.Port}");
            builder.AppendLine($"Path: {this.Path}");
            if (this.Query != null)
            {
                builder.AppendLine($"Query: {this.Query}");
                if (this.Fragment != null)
                {
                    builder.AppendLine($"Fragment: {this.Fragment}");
                }
            }
            return builder.ToString();
        }
    }
}
