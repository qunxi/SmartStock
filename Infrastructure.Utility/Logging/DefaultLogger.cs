using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utility.Logging
{
    public class DefaultLogger : ILogger
    {
        public void Debug(string message)
        {
            System.Diagnostics.Debug.Print(message);
        }

        public void Info(string message)
        {
            System.Diagnostics.Debug.Print(message);
        }

        public void Error(string message)
        {
            System.Diagnostics.Debug.Print(message);
        }

        public void Trace(string message)
        {
            System.Diagnostics.Debug.Print(message);
        }

        public void Warn(string message)
        {
            System.Diagnostics.Debug.Print(message);
        }

        public void Fatal(string message)
        {
            System.Diagnostics.Debug.Print(message);
        }
    }
}
