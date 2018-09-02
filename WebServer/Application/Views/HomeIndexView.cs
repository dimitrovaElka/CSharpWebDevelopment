namespace WebServer.Application.Views
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Server.HTTP.Contracts;

    public class HomeIndexView : IView
    {
        public string View()
        {
            return "<body><h1>Welcome</h1></body>";
        }
    }
}
