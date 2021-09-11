using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;

namespace APSystem.Core.Configuration
{
    
    public static class ConfigureFordwardHeaderOptions 
    {
       /// <summary>
       /// This 
       /// </summary>
       /// <value></value>
        public static void ConfigureService(IServiceCollection services)
        {
             services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

        }
    }
}