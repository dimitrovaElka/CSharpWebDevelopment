namespace WebServer.Application.Views.Home
{
    using Server.HTTP.Contracts;

    public class IndexView : IView
    {
        public string View()
        {
            return "<body><h1>Welcome</h1></body>";
        }
    }
}
