namespace SIS.HTTP.Cookies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class HttpCookieCollection : IHttpCookieCollection
    {
        private readonly IDictionary<string, ICollection<HttpCookie>> cookies;

        public HttpCookieCollection()
        {
            this.cookies = new Dictionary<string, ICollection<HttpCookie>>();
        }

        public void Add(HttpCookie cookie)
        {
            var cookieKey = cookie.Key;
            if (!this.cookies.ContainsKey(cookieKey))
            {
                this.cookies[cookieKey] = new List<HttpCookie>();
            }
            this.cookies[cookieKey].Add(cookie);
        }

        public bool ContainsCookie(string key)
        {
            return this.cookies.ContainsKey(key);
        }

        public HttpCookie GetCookie(string key)
        {
            return this.cookies[key].FirstOrDefault();
        }

        public bool HasCookies()
        {
            return this.cookies.Count > 0;
        }

        public override string ToString()
        {
            return string.Join("; ", this.cookies.Values);
        }
    }
}
