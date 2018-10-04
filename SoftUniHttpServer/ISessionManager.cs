
namespace SoftUniHttpServer
{
    public interface ISessionManager
    {
        void CreateSession(string sessionId);

        int GetSession(string sessionId);

        void SetSessionData(string sessionId, int newValue);

        bool Exists(string sessionId);
    }
}
