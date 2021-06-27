using System;
using APSystem.Configuration.Constants;
using APSystem.Configuration.Settings;
using APSystem.Data.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace APSystem.Core.Configuration
{
    public static class ConfigurationOptions
    {
        /// <summary>
        /// Configures the service.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public static void ConfigureService(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionSettings>(configuration.GetSection(DefaultConstants.ConnectionStrings));
            services.Configure<AppSettings>(configuration.GetSection(DefaultConstants.AppSettings));
            services.Configure<EmailSettings>(configuration.GetSection(DefaultConstants.EmailSettings));
            services.Configure<JwtSettings>(configuration.GetSection(DefaultConstants.JwtSettings));
            // services.AddIdentity<Microsoft.AspNetCore.Identity.IdentityUser, Microsoft.AspNetCore.Identity.IdentityRole>()
            //  .AddEntityFrameworkStores<ApDbContext>()
            // .AddDefaultTokenProviders();
            services.AddDbContext<ApDbContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            // services.AddScoped(typeof(IAuthRepository), typeof(AuthRepository));
            //services.AddScoped(typeof(IMasterDataRepository), typeof(MasterDataRepository));

        }
    }
}