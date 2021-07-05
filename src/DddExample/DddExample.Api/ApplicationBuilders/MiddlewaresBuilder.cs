using DddExample.Api.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace DddExample.Api.ApplicationBuilders
{
    public static class MiddlewaresBuilder
    {
        public static IApplicationBuilder UseCustomMiddlewaresDependencies(this IApplicationBuilder app)
        {
            app.UseGlobalExceptionHandlerMiddleware();
            app.UseUniqueRequestIdentifierMiddleware();
            app.UseEntryPointMiddleware();

            return app;
        }
    }
}
