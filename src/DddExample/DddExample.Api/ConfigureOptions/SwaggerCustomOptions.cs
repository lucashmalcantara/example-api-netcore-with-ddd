using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DddExample.Api.ConfigureOptions
{
    public class SwaggerCustomOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public SwaggerCustomOptions(IApiVersionDescriptionProvider provider) => 
            _provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "DddExample.Api",
                Version = description.ApiVersion.ToString(),
                Description = "This API applies the concepts of DDD and other cool stuff.",
            };

            if (description.IsDeprecated)
                info.Description += " [This API version is deprecated]";

            return info;
        }
    }
}
