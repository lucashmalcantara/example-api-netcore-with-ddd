using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DddExample.Api.ApplicationBuilders
{
    public static class HealthCheckBuilder
    {
        public static IApplicationBuilder UseCustomHealthChecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/health/liveness", new HealthCheckOptions()
            {
                Predicate = (_) => true
            });

            return app;
        }
    }
}
