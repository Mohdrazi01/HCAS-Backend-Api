using System;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;

namespace APSystem.Core.Configuration
{
    public static class ConfigureFormOptions
    {
       /// <summary>
       /// This 
       /// </summary>
       /// <value></value>
        public static void ConfigureService(IServiceCollection services)
        {
            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = int.MaxValue; //not recommended value
                options.MultipartBodyLengthLimit = long.MaxValue; //not recommended value
                options.MemoryBufferThreshold = int.MaxValue;
            });

        }
    }
}