using System;

namespace NZBDash.Common
{
    public interface ILogger
    {
        void Info(string message);
        void Warn(string message);
        void Debug(string message);
        void Error(string message);
        void Error(string message, Exception x);
        void Error(Exception x);
        void Fatal(string message);
        void Fatal(Exception x);
    }
}
