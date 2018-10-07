namespace SIS.HTTP.Sessions.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IHttpSession
    {
        string Id { get; }

        object GetParameter(string name);

        bool ContainsParameter(string name);

        void AddParameter(string name, object parameter);

        void ClearParameters();
    }
}
