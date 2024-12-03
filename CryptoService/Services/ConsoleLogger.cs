using System;

namespace CryptoService.Services
{
    public class ConsoleLogger : ILogger
    {
        public void Info(string message)
        {
            Log("INFO", message);
        }

        public void Error(string message, Exception ex = null)
        {
            Log("ERROR", message, ex);
        }

        public void Warning(string message)
        {
            Log("WARNING", message);
        }

        public void Debug(string message)
        {
            Log("DEBUG", message);
        }

        private void Log(string level, string message, Exception ex = null)
        {
            var logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";

            if (ex != null)
            {
                logMessage += Environment.NewLine + ex.ToString();
            }

            Console.WriteLine(logMessage);
        }
    }
}
