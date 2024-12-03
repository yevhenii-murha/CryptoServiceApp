using System;

namespace CryptoService.Services
{
    public interface ILogger
    {
        void Info(string message);
        void Error(string message, Exception ex = null);
        void Warning(string message);
        void Debug(string message);
    }
}