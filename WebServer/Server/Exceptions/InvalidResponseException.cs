namespace WebServer.Server.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class InvalidResponseException : Exception
    {
        public InvalidResponseException(string message)
            : base(message)
        {

        }
    }
}
