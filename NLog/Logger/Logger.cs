using System;
using NLog;

namespace Hdd.Logger
{
    public class Logger : ILogger
    {
        private readonly NLog.Logger _logger;

        public Logger(string name)
        {
            _logger = LogManager.GetLogger(name);
        }

        public void Trace(string message)
        {
            _logger.Trace(message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Debug(Exception exception)
        {
            _logger.Debug(exception);
        }

        public void Error(Exception exception)
        {
            _logger.Error(exception);
        }

        public void Fatal(Exception exception)
        {
            _logger.Fatal(exception);
        }

        public void Info(Exception exception)
        {
            _logger.Info(exception);
        }

        public void Trace(Exception exception)
        {
            _logger.Trace(exception);
        }

        public void Warn(Exception exception)
        {
            _logger.Warn(exception);
        }
    }
}