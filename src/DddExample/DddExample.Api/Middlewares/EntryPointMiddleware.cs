using DddExample.Infrastructure.Logging.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Threading.Tasks;

namespace DddExample.Api.Middlewares
{
    public class EntryPointMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IBasicLogger<EntryPointMiddleware> _logger;

        public EntryPointMiddleware(RequestDelegate next, IBasicLogger<EntryPointMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation(nameof(Invoke), $"Request URL: {context.Request.GetDisplayUrl()}", null);
            await _next(context);
        }
    }

    public static class EntryPointMiddlewareExtensions
    {
        public static IApplicationBuilder UseEntryPointMiddleware(this IApplicationBuilder builder) =>
            builder.UseMiddleware<EntryPointMiddleware>();
    }
}