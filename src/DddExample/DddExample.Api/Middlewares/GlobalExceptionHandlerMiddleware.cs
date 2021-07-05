using DddExample.Domain.Core.Results;
using DddExample.Infrastructure.Logging.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace DddExample.Api.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IBasicLogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, IBasicLogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                var statusCode = HttpStatusCode.InternalServerError;

                _logger.LogException(nameof(Invoke), $"Request URL: {context.Request.GetDisplayUrl()}. Produced status code {statusCode}", exception, null);

                var errorResult = SimpleResult.Error(new Error(exception.ToString()));

                await WriteResponseAsync(context, statusCode, errorResult);
            }
        }

        private async Task WriteResponseAsync(HttpContext context, HttpStatusCode statusCode, SimpleResult result)
        {
            context.Response.Clear();
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(
                JsonSerializer.Serialize(result.Errors));
        }
    }

    public static class GlobalExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandlerMiddleware(this IApplicationBuilder builder) =>
            builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }
}