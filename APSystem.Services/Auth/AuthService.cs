using System.Net.Http;
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
using APSystem.Data.Model;

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
        , IAuthRepository authRepository
        , IMetaDataService metaDataService
        , ILogger<AuthService> logger) : base(metaDataService, logger)
        {

            _authRepository = authRepository;
            _appSettings = appSettings;
            _hostingEnvironment = hostingEnvironment;
            _jwtSettings = jwtSettings;
            _emailService = emailService;
        }

        async Task<AuthResponse> IAuthService.Login(AuthRequest request)
        {
            AuthResponse authResponse = new AuthResponse();
            var user = await _authRepository.GetUser(request.UserName);
            if (user == null || user.IsActive == false)
                throw new AppException(Models.Enums.AuthCodes.E6002);
            if (user.IsEmailConfirmed == false)
                throw new AppException(Models.Enums.AuthCodes.E6002);
            var hasher = new PasswordHasher<UsersDbEntity>();
            var verifyPassword = hasher.VerifyHashedPassword(user, user.Password, request.Password);
            if (verifyPassword == PasswordVerificationResult.Failed)
            {
                throw new AppException(Models.Enums.AuthCodes.E6005);
            }
            var token = GenerateJwtToken(user);
            authResponse.UserId = user.UserID;
            authResponse.UserName = user.UserName;
            authResponse.FullName = user.Name;
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
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
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
            string url = $"{_appSettings.Value.BaseUrl}activate?emailActivationCode={emailActivationCode}";
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

        async Task<UserDetailsResponse> IAuthService.GetUser(int UserID)
        {
            var userbyid = await _authRepository.GetUser(UserID);
            UserDetailsResponse userDetailResponseByUserID = new UserDetailsResponse()
            {
                UserID = userbyid.UserID,
                Name = userbyid.Name,
                RoleID = userbyid.RoleID,
                RoleName = userbyid.RoleName,
                Email = userbyid.Email,
                PhoneNumber = userbyid.PhoneNumber,
                DateOfBirth = userbyid.DateOfBirth,
                Gender = userbyid.Gender,
                GenderName = userbyid.GenderName,
                GMCNumber = userbyid.GMCNumber,
                Speciality = userbyid.Speciality,
                Experience = userbyid.Experience,
                Address = userbyid.Address
            };

            return await Task.FromResult(userDetailResponseByUserID);
        }

        async Task<List<UserDetailsResponse>> IAuthService.GetAllUsersbyRole(int roleID)
        {
            var usersbyrole = await _authRepository.GetAllUsersbyRole(roleID);
            List<UserDetailsResponse> userDetailResponsesByRoleID = new List<UserDetailsResponse>();
            foreach (UserModel us in usersbyrole)
            {
                userDetailResponsesByRoleID.Add(new UserDetailsResponse
                {
                    UserID = us.UserID,
                    Name = us.Name,
                    RoleID = us.RoleID,
                    RoleName = us.RoleName,
                    Email = us.Email,
                    PhoneNumber = us.PhoneNumber,
                    DateOfBirth = us.DateOfBirth,
                    Gender = us.Gender,
                    GenderName = us.GenderName,
                    GMCNumber = us.GMCNumber,
                    Speciality = us.Speciality,
                    Experience = us.Experience,
                    Address = us.Address
                });
            }
            return await Task.FromResult(userDetailResponsesByRoleID);
        }

        async Task<UserDetailsResponse> IAuthService.UpdateUserDetails(int id, UserDetailsRequest userbyid)
        {
            UserDetailsResponse userresp = new UserDetailsResponse();
            UserModel updateUser = new UserModel()
            {
                Name = userbyid.Name,
                Email = userbyid.Email,
                PhoneNumber = userbyid.PhoneNumber,
                DateOfBirth = userbyid.DateOfBirth,
                Gender = userbyid.Gender,
                GMCNumber = userbyid.GMCNumber,
                Speciality = userbyid.Speciality,
                Experience = userbyid.Experience,
                Address = userbyid.Address
            };
            var user = _authRepository.UpdateUserDetails(id, updateUser);

            return await Task.FromResult(userresp);

        }



        async Task<List<UserDetailsResponse>> IAuthService.GetAllDoctorsandNurses()
        {
            var usersbyrole = await _authRepository.GetAllDoctorsandNurses();
            List<UserDetailsResponse> userDetailResponsesByRoleID = new List<UserDetailsResponse>();
            foreach (UserModel us in usersbyrole)
            {
                userDetailResponsesByRoleID.Add(new UserDetailsResponse
                {
                    UserID = us.UserID,
                    Name = us.Name,
                    RoleID = us.RoleID,
                    RoleName = us.RoleName,
                    Email = us.Email,
                    PhoneNumber = us.PhoneNumber,
                    DateOfBirth = us.DateOfBirth,
                    Gender = us.Gender,
                    GenderName = us.GenderName,
                    GMCNumber = us.GMCNumber,
                    Speciality = us.Speciality,
                    Experience = us.Experience,
                    Address = us.Address
                });
            }
            return await Task.FromResult(userDetailResponsesByRoleID);
        }

        async Task<List<UserDetailsResponse>> IAuthService.GetAllUsers()
        {
            var usersbyrole = await _authRepository.GetAllUsers();
            List<UserDetailsResponse> userDetailsResponses = new List<UserDetailsResponse>();
            foreach (UserModel us in usersbyrole)
            {
                userDetailsResponses.Add(new UserDetailsResponse
                {
                    UserID = us.UserID,
                    Name = us.Name,
                    RoleID = us.RoleID,
                    RoleName = us.RoleName,
                    Email = us.Email,
                    PhoneNumber = us.PhoneNumber,
                    DateOfBirth = us.DateOfBirth,
                    Gender = us.Gender,
                    GenderName = us.GenderName,
                    GMCNumber = us.GMCNumber,
                    Speciality = us.Speciality,
                    Experience = us.Experience,
                    Address = us.Address
                });
            }
            return await Task.FromResult(userDetailsResponses);
        }

        async Task<List<RoleResponse>> IAuthService.GetRoles()
        {
            var userRoles = await _authRepository.GetRoles();
            List<RoleResponse> userRolesList = new List<RoleResponse>();
            foreach (RolesModelItem r in userRoles)
            {
                userRolesList.Add(new RoleResponse
                {
                    RoleID = r.RoleID,
                    RoleName = r.RoleName
                });
            }
            return await Task.FromResult(userRolesList);
        }

        async Task<List<GenderResponse>> IAuthService.GetGender()
        {
            var userGender = await _authRepository.GetGender();
            List<GenderResponse> genderResponses = new List<GenderResponse>();
            foreach (GenderModel r in userGender)
            {
                genderResponses.Add(new GenderResponse
                {
                    GenderId = r.GenderId,
                    GenderName = r.GenderName
                });
            }
            return await Task.FromResult(genderResponses);
        }

        async Task<EmailConfirmationResponse> IAuthService.UsersEmailConfirmation(string activationCode)
        {
            var userConfirmation = await _authRepository.UsersEmailConfirmation(activationCode);
            return await Task.FromResult(new EmailConfirmationResponse());
        }
    }
}