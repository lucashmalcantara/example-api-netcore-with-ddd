using DddExample.Infrastructure.WebApi.Constants;
using Microsoft.AspNetCore.Http;

namespace DddExample.Infrastructure.WebApi.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static string GetCorrelationId(this IHttpContextAccessor httpContext) =>
            httpContext.HttpContext?.Items[HeaderNames.CorrelationId]?.ToString();
    }
}
