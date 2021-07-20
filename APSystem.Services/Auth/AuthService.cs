using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
using Microsoft.IdentityModel.Tokens;

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
        , IWebHostEnvironment hostingEnvironment
        , IOptions<JwtSettings> jwtSettings
        , IEmailService emailService
        //,IUserRepository userRepo
        //,IHttpContextAccessor accessor
        , IAuthRepository authRepository
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
            AuthResponse authResponse = new AuthResponse();
            var user = await _authRepository.GetUser(request.UserName);
            if (user == null || user.IsActive == false)
                throw new UnauthorizedAccessException();
            if (user.IsEmailConfirmed == false)
                throw new AppException(Models.Enums.AuthCodes.E6009);
            var hasher = new PasswordHasher<UsersDbEntity>();
            var verifyPassword = hasher.VerifyHashedPassword(user, user.Password, request.Password);
            if (verifyPassword == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedAccessException();
            }
            var token = GenerateJwtToken(user);
            authResponse.UserId = user.UserID;
            authResponse.UserName = user.UserName;
            authResponse.RoleId = user.RoleID;
            authResponse.Access_Token = token;
            authResponse.IsActive = user.IsActive;
            authResponse.IsEmailConfirmed = user.IsEmailConfirmed;
            return await Task.FromResult(authResponse);
        }

        private string GenerateJwtToken(UsersDbEntity user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Value.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]{
                new Claim("UserId",user.UserID.ToString()),
                 new Claim("UserName",user.UserName.ToString()),
                  new Claim("Email",user.Email.ToString()),
                   new Claim("RoleId",user.RoleID.ToString()),
                    new Claim("IsActive",user.IsActive.ToString()),
                     new Claim("Name",user.Name.ToString()),
                       new Claim("IsEmailConfirmed",user.IsEmailConfirmed.ToString()),
                         new Claim("EmailActivationCode",user.EmailActivationCode.ToString()),
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };
              var token = tokenHandler.CreateToken(tokenDescriptor);
              return tokenHandler.WriteToken(token);
        }

        async Task<RegisterUserResponse> IAuthService.RegisterUser(RegisterUserRequest request)
        {
            RegisterUserResponse registerUserResponse = new RegisterUserResponse();
            var UserDbEntity = request.ToUserService(); //Extension
            var hasher = new PasswordHasher<UsersDbEntity>();
            var passwordHash = hasher.HashPassword(UserDbEntity, UserDbEntity.Password);
            UserDbEntity.Password = passwordHash;
            var dbResponse = await _authRepository.CreateUser(UserDbEntity);
            dbResponse.Password = string.Empty;
            if (dbResponse.IsUserCreated)
            {
                await SendEmailAsync(dbResponse.Email, dbResponse.EmailActivationCode.Value.ToString());
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