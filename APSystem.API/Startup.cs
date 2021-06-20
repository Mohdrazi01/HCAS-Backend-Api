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

namespace APSystem.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get;}

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
             services.AddControllers()
             .AddNewtonsoftJson(options=>options.SerializerSettings.ContractResolver= new CamelCasePropertyNamesContractResolver());
             services.AddApplicationInsightsTelemetry();
             ConfigurationOptions.ConfigureService(services,Configuration);
             ConfigurationCorsOptions.ConfigureService(services);
             ConfigureFormOptions.ConfigureService(services);
             ConfigureFordwardHeaderOptions.ConfigureService(services);
             SwaggerConfiguration.ConfigureService(services);
             ServiceRegisterationConfiguration.ConfigureService(services,Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else{
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
               var builder = endpoints.MapControllers();
               builder.RequireCors("CorsPolicy");
            });
        }
    }
}
