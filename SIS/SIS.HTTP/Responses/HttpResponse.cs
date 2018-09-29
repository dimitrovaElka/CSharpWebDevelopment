namespace SIS.HTTP.Responses
{
    using System;
    using System.Linq;
    using System.Text;
    using Common;
    using Enums;
    using Extensions;
    using Headers;

    public class HttpResponse : IHttpResponse
    {
        public HttpResponse() {}

        public HttpResponse(HttpResponseStatusCode statusCode)
        {
            this.Headers = new HttpHeaderCollection();
            this.Content = new byte[0];
            this.StatusCode = statusCode;
        }

        public HttpResponseStatusCode StatusCode { get; set; }

        public IHttpHeaderCollection Headers { get; private set; }

        public byte[] Content { get; set; }

        public void AddHeader(HttpHeader header)
        {
            CoreValidator.ThrowIfNull(header, nameof(header));
            this.Headers.Add(header);
        }

        public byte[] GetBytes()
        {
            //byte[] array = Encoding.UTF8.GetBytes(this.ToString());

            //this.Content = this.Content.Concat(array).ToArray();
            //return array;

            return Encoding.UTF8.GetBytes(this.ToString()).Concat(this.Content).ToArray();
        }

        public override string ToString()
        {
            StringBuilder response = new StringBuilder();

            response.AppendLine($"{GlobalConstants.HttpOneProtokolFragment} {this.StatusCode.GetResponseLine()}");

            response.AppendLine(this.Headers.ToString());

            return response.ToString();
        }
    }
}
