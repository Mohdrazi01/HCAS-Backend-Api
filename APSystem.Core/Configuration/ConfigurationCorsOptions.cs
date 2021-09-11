using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace APSystem.Core.Configuration
{
    public static class ConfigurationCorsOptions
    {
       /// <summary>
       /// This
       /// </summary>
       /// <value></value>
        public static void ConfigureService(IServiceCollection services)
        {
            services.AddCors(options =>
                        {
                            options.AddPolicy("CorsPolicy",
                            builder => builder.WithOrigins("http://localhost:4200","http://localhost:4300")
                                            //.AllowAnyOrigin()
                                            .AllowAnyMethod()
                                            .AllowAnyHeader()
                                            .AllowCredentials()
                                            .Build());
                        });
        }
    }
}