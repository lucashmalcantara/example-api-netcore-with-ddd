using DddExample.Api.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace DddExample.Api.ApplicationBuilders
{
    public static class SwaggerBuilder
    {
        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                    c.SwaggerEndpoint($"{ApplicationConstants.ApplicationPathBase}/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            });

            return app;
        }
    }
}
