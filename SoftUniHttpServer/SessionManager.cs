namespace SoftUniHttpServer
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Text;

    public class SessionManager : ISessionManager
    {
        // private readonly IDictionary<string, IEnumerable<KeyValuePair<string,string>>> sessionData = new ConcurrentDictionary<string, IEnumerable<KeyValuePair<string, string>>>();

        private readonly IDictionary<string, int> sessionData = new ConcurrentDictionary<string, int>();

        public void CreateSession(string sessionId)
        {
            this.sessionData.Add(sessionId, 0);
        }

        public int GetSession(string sessionId)
        {
            this.sessionData.TryGetValue(sessionId, out int value);
            return value;
        }

        public void SetSessionData(string sessionId, int newValue)
        {
            this.sessionData[sessionId] = newValue;
        }

        public bool Exists(string sessionId)
        {
            return this.sessionData.TryGetValue(sessionId, out _);
        }
    }
}
