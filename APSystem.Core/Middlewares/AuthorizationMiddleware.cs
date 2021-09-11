using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APSystem.Configuration.Settings;
using APSystem.Models.Auth;
using APSystem.Models.Enums;
using APSystem.Services.Auth;
using APSystem.Services.MetaData;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<JwtSettings> _jwtSettings;
        public AuthorizationMiddleware(RequestDelegate next
                            , IHttpContextAccessor httpContextAccessor
                            , IOptions<JwtSettings> jwtSettings)
        {
            _next = next;
            _httpContextAccessor = httpContextAccessor;
            _jwtSettings = jwtSettings;
        }

        public async Task Invoke(HttpContext httpContext, IAuthService authService, IMetaDataService metaDataService)
        {
            
            if (!_httpContextAccessor.HttpContext.Request.Path.StartsWithSegments("/api/v1/Auth") )
            {

                var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (token == null)
                {
                    httpContext.Response.StatusCode = 401;
                    throw new UnauthorizedAccessException();
                }
                attachUserToContext(httpContext, authService, token, metaDataService);
            }
            await _next(httpContext);
        }

        private void attachUserToContext(HttpContext httpContext, IAuthService authService, string token, IMetaDataService metaDataService)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSettings.Value.SecretKey);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "UserId").Value);

                if (userId < 1)
                {
                    httpContext.Response.StatusCode = 401;
                    throw new UnauthorizedAccessException();
                }
                var userName = jwtToken.Claims.FirstOrDefault(x => x.Type == "UserName").Value;
                var email = jwtToken.Claims.FirstOrDefault(x => x.Type == "Email").Value;
                var roleId = int.Parse(jwtToken.Claims.FirstOrDefault(x => x.Type == "RoleId").Value);
                var isActive = bool.Parse(jwtToken.Claims.FirstOrDefault(x => x.Type == "IsActive").Value);
                var name = jwtToken.Claims.FirstOrDefault(x => x.Type == "Name").Value;
                var isEmailConfirmed = bool.Parse(jwtToken.Claims.FirstOrDefault(x => x.Type == "IsEmailConfirmed").Value); 
                var emailActivationCode = jwtToken.Claims.FirstOrDefault(x => x.Type == "EmailActivationCode").Value;
                AuthResponse authorizationResult = new AuthResponse()
                {
                    Email = email,
                    UserId = userId,
                    UserName = userName,
                    RoleId = roleId,
                    IsActive = isActive,
                    FullName = name,
                };
                metaDataService.SetAuth(authorizationResult);
                metaDataService.SetAttribute(MetaDataEnum.CLIENT_IP_ADDRESS.ToString(), httpContext.Connection.RemoteIpAddress.ToString());
            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException();
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }