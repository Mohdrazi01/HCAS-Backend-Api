using System;
using APSystem.Services.Auth;
using APSystem.Services.MetaData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace APSystem.Core.Configuration
{
    public static class  ServiceRegisterationConfiguration
    {
       /// <summary>
        /// Configures the service.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureService(IServiceCollection services,IConfiguration configuration)
        {
              #region  DI
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
           // services.AddScoped<Microsoft.AspNetCore.Identity.UserManager<IdentityUser>>();
           // services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
           // services.AddScoped(typeof(IAuthRepository), typeof(AuthRepository));
            services.AddScoped(typeof(IAuthService), typeof(AuthService));
           // services.AddScoped(typeof(IMasterDataService), typeof(MasterDataService));
            services.AddScoped(typeof(IMetaDataService), typeof(MetaDataService));
           // services.AddScoped(typeof(IRequestModelValidationRules<>), typeof(RequestModelValidationRules<>));
           // services.AddScoped(typeof(IRequestValidationService<>), typeof(RequestValidationService<>));
           // services.AddScoped(typeof(IEmailService), typeof(EmailService));
            #endregion

        }
    }
}