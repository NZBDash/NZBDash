using System;

namespace NZBDash.Common.Interfaces
{
    public interface ILogger
    {
        void Trace(string message);
        void Trace(string message, params object[] args);
        void Info(string message);
        void Info(string message, params object[] args);
        void Warn(string message);
        void Warn(string message, params object[] args);  
        void Debug(string message);
        void Debug(string message, params object[] args);
        void Error(string message);
        void Error(string message, params object[] args);
        void Error(string message, Exception x);
        void Error(Exception x, string message, params object[] args);
        void Error(Exception x);
        void Fatal(string message);
        void Fatal(string message, params object[] args);
        void Fatal(Exception x);
        void Fatal(Exception x, string message, params object[] args);
    }
}
