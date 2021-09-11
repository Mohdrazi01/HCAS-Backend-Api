using System;
using APSystem.Data.Repositories.Auth;
using APSystem.Data.Repositories.Appointment;
using APSystem.Services.Auth;
using APSystem.Services.Email;
using APSystem.Services.MetaData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using APSystem.Services.Appointment;
using APSystem.Services.Bookings;
using APSystem.Data.Repositories.BookingAppointment;
using APSystem.Services.Sms;
using Twilio.Clients;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace APSystem.Core.Configuration
{
    public static class ServiceRegisterationConfiguration
    {
        /// <summary>
        /// Configures the service.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureService(IServiceCollection services, IConfiguration configuration)
        {
            #region  DI
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // services.AddScoped<Microsoft.AspNetCore.Identity.UserManager<IdentityUser>>();
            services.AddScoped(typeof(IAuthRepository), typeof(AuthRepository));
            services.AddScoped(typeof(IAuthService), typeof(AuthService));
            services.AddScoped(typeof(IAppointmentRepository), typeof(AppointmentRepository));
            services.AddScoped(typeof(IAppointmentService), typeof(AppointmentService));
            services.AddScoped(typeof(IBookingsRepository), typeof(BookingsRepository));
            services.AddScoped(typeof(IBookingsService), typeof(BookingService));
            // services.AddScoped(typeof(IMasterDataService), typeof(MasterDataService));
            services.AddScoped(typeof(IMetaDataService), typeof(MetaDataService));
            // services.AddScoped(typeof(IRequestModelValidationRules<>), typeof(RequestModelValidationRules<>));
            // services.AddScoped(typeof(IRequestValidationService<>), typeof(RequestValidationService<>));
            services.AddScoped(typeof(IEmailService), typeof(EmailService));
             services.AddScoped(typeof(ISmsService), typeof(SmsService));
          //   services.AddScoped(typeof(ITwilioRestClient), typeof(SmsService));
            services.AddHttpClient<ITwilioRestClient, TwilioClient>();
            #endregion

        }
    }
}