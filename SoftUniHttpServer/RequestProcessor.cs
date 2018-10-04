
namespace SoftUniHttpServer
{
    using System;
    using System.IO;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    public class RequestProcessor
    {
        SessionManager sessionManager = new SessionManager();

        object lockObject = new object();

        public  async Task ProcessClient(TcpClient client)
        {
            var buffer = new byte[10240];

            var stream = client.GetStream();

            //Console.WriteLine($"{client.Client.RemoteEndPoint} {Thread.CurrentThread.ManagedThreadId}");
            var readLength = await stream.ReadAsync(buffer, 0, buffer.Length);

            var requestText = Encoding.UTF8.GetString(buffer, 0, readLength);
            var sessionId = ParseSessionId(requestText);

            //await Task.Run(() => Thread.Sleep(10000));
            Console.WriteLine(new string('=', 30));
            Console.WriteLine(requestText);

            string sessionSetCookie = null;
            if (!sessionManager.Exists(sessionId))
            {
                string newSessionId = Guid.NewGuid().ToString();
                sessionManager.CreateSession(newSessionId);
                sessionManager.GetSession(newSessionId);
                sessionSetCookie = "Set-Cookie: SessionId=" + newSessionId + "; Max-Age=86000000" + Environment.NewLine;
            }
            else
            {
                lock(lockObject)
                {
                    var data = sessionManager.GetSession(sessionId);
                    data++;
                    sessionManager.SetSessionData(sessionId, data);
                }
            }

            var sessionData = sessionManager.GetSession(sessionId);
            var responseText = "Hello, " + sessionId + " " + sessionData;    //File.ReadAllText("index.html");

            var responseBytes = Encoding.UTF8.GetBytes(
                 "HTTP/1.0 200 OK" + Environment.NewLine +
                 "Content-Type: text/plain" + Environment.NewLine +
                 "Content-Length: " + responseText.Length + Environment.NewLine + sessionSetCookie +
                 "Set-Cookie: cookie=OK; Expires=" + DateTime.UtcNow.AddMinutes(1).ToString("R") +
                 Environment.NewLine + Environment.NewLine +
                 responseText
                );

            await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
          //  Console.WriteLine($"{client.Client.RemoteEndPoint} {Thread.CurrentThread.ManagedThreadId}");
        }

        private string ParseSessionId(string request)
        {
            StringReader sr = new StringReader(request);
            string line = string.Empty;

            while ((line = sr.ReadLine()) != null)
            {
                if (line.StartsWith("Cookie: "))
                {
                    var lineParts = line.Split(": ", StringSplitOptions.RemoveEmptyEntries);

                    if (lineParts.Length == 2)
                    {
                        var cookies = lineParts[1].Split("; ", StringSplitOptions.RemoveEmptyEntries);

                        foreach (var cookie in cookies)
                        {
                            var cookieParts = cookie.Split('=', 2, StringSplitOptions.RemoveEmptyEntries);

                            if (cookieParts.Length == 2)
                            {
                                var cookieName = cookieParts[0];
                                var cookieValue = cookieParts[1];
                                if (cookieName == "SessionId")
                                {
                                    return cookieValue;
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }
    }
}
