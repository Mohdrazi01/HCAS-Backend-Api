using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using APSystem.Core.Configuration;
using APSystem.Core.Controllers.Chat;
using APSystem.Core.Hubs;

namespace APSystem.API
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
            .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
            services.AddApplicationInsightsTelemetry();
            ConfigurationOptions.ConfigureService(services, _configuration);
            ConfigurationCorsOptions.ConfigureService(services);
            ConfigureFormOptions.ConfigureService(services);
            ConfigureFordwardHeaderOptions.ConfigureService(services);
            SwaggerConfiguration.ConfigureService(services);
            ServiceRegisterationConfiguration.ConfigureService(services, _configuration);
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            //Swagger Configuration
            SwaggerConfiguration.Configure(app);

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            MiddlewareConfiguration.Configure(app);
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/api/v1/Auth/ChatHub");
                var builder = endpoints.MapControllers();
                builder.RequireCors("CorsPolicy");

            });
        }
    }
}
