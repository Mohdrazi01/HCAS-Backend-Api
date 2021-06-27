using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using APSystem.Configuration.Settings;
using APSystem.Data.Entities;
using APSystem.Data.Repositories.Auth;
using APSystem.Models.Auth;
using APSystem.Models.Email;
using APSystem.Services.Email;
using APSystem.Services.Extensions;
using APSystem.Services.MetaData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace APSystem.Services.Auth
{
    public class AuthService : BaseService<AuthService>, IAuthService
    {
        private IAuthRepository _authRepository;
        private readonly IOptions<AppSettings> _appSettings;
        //private readonly IUserRepository _userRepo;
        //private IHttpContextAccessor _accessor;
        private readonly IEmailService _emailService;
        IOptions<JwtSettings> _jwtSettings;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public AuthService(IOptions<AppSettings> appSettings 
        ,IWebHostEnvironment hostingEnvironment
        ,IOptions<JwtSettings> jwtSettings
        ,IEmailService emailService
        //,IUserRepository userRepo
        //,IHttpContextAccessor accessor
        ,IAuthRepository authRepository
        , IMetaDataService metaDataService
        , ILogger<AuthService> logger) : base(metaDataService, logger)
        {

            _authRepository = authRepository;
            _appSettings = appSettings;
            _hostingEnvironment = hostingEnvironment;
            _jwtSettings = jwtSettings;
            _emailService = emailService;
            //_userRepo = userRepo;
            //_accessor = accessor;
        }

        async Task<AuthResponse> IAuthService.Login(AuthRequest request)
        {
            return await Task.FromResult(new AuthResponse());
        }

        async Task<RegisterUserResponse> IAuthService.RegisterUser(RegisterUserRequest request)
        {
            RegisterUserResponse registerUserResponse = new RegisterUserResponse();
            var patientDbEntity = request.ToPatientUserService();
            var hasher = new PasswordHasher<PatientDbEntity>();
            var passwordHash = hasher.HashPassword(patientDbEntity, patientDbEntity.Password);
            patientDbEntity.Password = passwordHash;
            var dbResponse = await _authRepository.CreatePatientUser(patientDbEntity);
            dbResponse.Password = string.Empty;
            if (dbResponse.IsUserCreated)
            {
                await SendEmailAsync(dbResponse.Email,dbResponse.EmailActivationCode.Value.ToString());
            }
            else
            {
                if (dbResponse.IsUserNameExist)
                {
                    throw new AppException(Models.Enums.AuthCodes.E6009);
                }
            }
            return await Task.FromResult(registerUserResponse);

        }


        private async Task SendEmailAsync(string email, string emailActivationCode)
        {
            string mailBody = string.Empty;
            string url = $"{_appSettings.Value.BaseUrl}activate?ActivationCode={emailActivationCode}";
            var fileStream = new FileStream(Path.Combine(_hostingEnvironment.ContentRootPath, "EmailContent/email-verification.html"), FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                mailBody = streamReader.ReadToEnd();
            }
            mailBody = mailBody.Replace("{url}", url);
            List<string> emails = new List<string>();
            emails.Add(email);
            EmailRequest emailRequest = new EmailRequest(
                         emails,
                         $"Email Confirmation",
                        mailBody,
                         null
                     );
            await _emailService.SendEmailAsync(emailRequest);
        }

    }
}