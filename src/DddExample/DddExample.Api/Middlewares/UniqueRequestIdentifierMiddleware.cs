using DddExample.Infrastructure.WebApi.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace DddExample.Api.Middlewares
{
    public class UniqueRequestIdentifierMiddleware
    {
        private readonly RequestDelegate _next;

        public UniqueRequestIdentifierMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            var correlationId = context.Request.Headers[HeaderNames.CorrelationId].ToString();

            if (string.IsNullOrWhiteSpace(correlationId))
                correlationId = Guid.NewGuid().ToString();

            context.Items.Add(HeaderNames.CorrelationId, correlationId);

            context.Response.Headers.Add(
                HeaderNames.CorrelationId,
                context.Items[HeaderNames.CorrelationId].ToString());

            await _next(context);
        }
    }

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseUniqueRequestIdentifierMiddleware(this IApplicationBuilder builder) =>
            builder.UseMiddleware<UniqueRequestIdentifierMiddleware>();
    }
}
