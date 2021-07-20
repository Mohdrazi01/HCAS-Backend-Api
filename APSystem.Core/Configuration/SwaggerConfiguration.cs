using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace APSystem.Core.Configuration
{
    public static class  SwaggerConfiguration
    {
      /// <summary>
        /// Configures the service.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureService(IServiceCollection services)
        {
              // Swagger API documentation
            services.AddSwaggerGen(c =>
            {
                // c.DocInclusionPredicate((_, api) => !string.IsNullOrWhiteSpace(api.GroupName));

                // c.TagActionsBy(api => api.GroupName);
                c.SwaggerDoc("v1",
                                new OpenApiInfo
                                {
                                    Title = "APMS API",
                                    Version = "v1.0",
                                    Description = "APMS API",
                                    TermsOfService = new Uri("https://Apms.com"),
                                    Contact = new OpenApiContact
                                    {
                                        Name = "APMS",
                                        Email = "support@APMS.com"
                                    },
                                    License = new OpenApiLicense
                                    {
                                        Name = "APMS",
                                        Url = new Uri("https://APMS.com")
                                    },

                                });
            c.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below."

            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
            });
            services.ConfigureSwaggerGen(options =>
             {
                      // UseFullTypeNameInSchemaIds replacement for .NET Core
                        options.CustomSchemaIds(x => x.FullName);
             });

        }

         /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public static void Configure(IApplicationBuilder app)
        {
            // This will redirect default url to Swagger url
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "APMS API");
            });
        }
    }
}