using DddExample.Infrastructure.Logging.Interfaces;
using DddExample.Infrastructure.Logging.Models;
using DddExample.Infrastructure.WebApi.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;

namespace DddExample.Infrastructure.Logging
{
    public class BasicLogger<T> : IBasicLogger<T>
    {
        private readonly ILogger<T> _logger;
        private readonly IHttpContextAccessor _httpContext;

        public BasicLogger(ILogger<T> logger, IHttpContextAccessor httpContext)
        {
            _logger = logger;
            _httpContext = httpContext;
        }

        public void LogCritical(string action, string message, object data)
        {
            var logObject = new LogObjectModel(
                _httpContext.GetCorrelationId(),
                action,
                message,
                data);

            var serializedLogObject = JsonSerializer.Serialize(logObject);

            _logger.LogCritical(serializedLogObject);
        }

        public void LogDebug(string action, string message, object data)
        {
            var logObject = new LogObjectModel(
                _httpContext.GetCorrelationId(),
                action,
                message,
                data);
            
            var serializedLogObject = JsonSerializer.Serialize(logObject);

            _logger.LogDebug(serializedLogObject);
        }

        public void LogError(string action, string message, object data)
        {
            var logObject = new LogObjectModel(
                _httpContext.GetCorrelationId(),
                action,
                message,
                data);

            var serializedLogObject = JsonSerializer.Serialize(logObject);

            _logger.LogError(serializedLogObject);
        }

        public void LogException(string action, Exception exception, object data)
        {
            var logObject = new LogObjectModel(
                _httpContext.GetCorrelationId(),
                action,
                exception.ToString(),
                data);

            var serializedLogObject = JsonSerializer.Serialize(logObject);

            _logger.LogError(serializedLogObject);
        }

        public void LogException(string action, string message, Exception exception, object data)
        {
            var logObject = new LogObjectModel(
                _httpContext.GetCorrelationId(),
                action,
                $"{message}\r\nException details: {exception}".Trim(),
                data);

            var serializedLogObject = JsonSerializer.Serialize(logObject);

            _logger.LogError(serializedLogObject);
        }

        public void LogInformation(string action, string message, object data)
        {
            var logObject = new LogObjectModel(
                _httpContext.GetCorrelationId(),
                action,
                message,
                data);

            var serializedLogObject = JsonSerializer.Serialize(logObject);

            _logger.LogInformation(serializedLogObject);
        }

        public void LogTrace(string action, string message, object data)
        {
            var logObject = new LogObjectModel(
                _httpContext.GetCorrelationId(),
                action,
                message,
                data);

            var serializedLogObject = JsonSerializer.Serialize(logObject);

            _logger.LogTrace(serializedLogObject);
        }

        public void LogWarning(string action, string message, object data)
        {
            var logObject = new LogObjectModel(
                _httpContext.GetCorrelationId(),
                action,
                message,
                data);

            var serializedLogObject = JsonSerializer.Serialize(logObject);

            _logger.LogWarning(serializedLogObject);
        }
    }
}
