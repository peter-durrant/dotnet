using System;

namespace Hdd.Logger
{
    public interface ILogger
    {
        void Debug(string message);
        void Error(string message);
        void Fatal(string message);
        void Info(string message);
        void Trace(string message);
        void Warn(string message);

        void Debug(Exception exception);
        void Error(Exception exception);
        void Fatal(Exception exception);
        void Info(Exception exception);
        void Trace(Exception exception);
        void Warn(Exception exception);

    }
}