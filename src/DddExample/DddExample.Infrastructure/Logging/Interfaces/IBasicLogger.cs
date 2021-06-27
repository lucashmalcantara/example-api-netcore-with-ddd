using System;

namespace DddExample.Infrastructure.Logging.Interfaces
{
    public interface IBasicLogger<T>
    {
        void LogCritical(string action, string message, object data);
        void LogDebug(string action, string message, object data);
        void LogError(string action, string message, object data);
        void LogException(string action, Exception exception, object data);
        void LogException(string action, string message, Exception exception, object data);
        void LogInformation(string action, string message, object data);
        void LogTrace(string action, string message, object data);
        void LogWarning(string action, string message, object data);
    }
}
