using System;
using Hdd.Logger;

namespace Hdd.Model
{
    public class Model
    {
        private readonly ILogger _log;

        public Model(ILogger log)
        {
            _log = log;
        }

        public bool Operate(bool raiseError)
        {
            _log.Info("About to operate...");

            try
            {
                if (raiseError)
                    throw new InvalidOperationException("ERROR!");
            }
            catch (Exception e)
            {
                _log.Fatal($"Error during operate: {e.Message}");
                return false;
            }

            _log.Info("Operate complete");
            return true;
        }
    }
}
