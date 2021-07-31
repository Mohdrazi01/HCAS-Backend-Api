using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using APSystem.Models.Auth;
using APSystem.Services.Auth;

namespace APSystem.Core.Controllers.Auth
{
    [Route("api/v1/Auth")]
    [ApiController]
    public class AuthController : BaseController<AuthController>
    {
        IAuthService _authService;
        public AuthController(ILogger<AuthController> logger,IAuthService authService): base(logger)
        {
            _authService = authService;
        }
        [HttpPost]
        [Route("signup")]
        public async Task<ActionResult<RegisterUserResponse>> RegisterUser([FromBody] RegisterUserRequest request)
        {
            var response =await _authService.RegisterUser(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] AuthRequest request)
        {
            var response = await _authService.Login(request);
            return response;
        }

        [HttpGet("GetAllUsers")]
         public async Task<ActionResult<List<UserDetailsResponse>>> GetUsers()
        {
            var listofUsers = await _authService.GetAllUsers();
            return listofUsers;
        }
        [HttpPost("UserbyID")]
         public async Task<ActionResult<UserDetailsResponse>> GetUserById(int userID)
        {
            var user = await _authService.GetUser(userID);
            return user;
        }
        [HttpPost("UsersbyRoleID")]
         public async Task<ActionResult<List<UserDetailsResponse>>> GetUsersByRoleID(int roleID)
        {
            var userbyRole = await _authService.GetAllUsersbyRole(roleID);
            return userbyRole;
        }
        [HttpGet("GetAllRoles")]
         public async Task<ActionResult<List<RoleResponse>>> GetAllRoles()
        {
            var roles = await _authService.GetRoles();
            return roles;
        }
        [HttpGet("GetAllGenders")]
         public async Task<ActionResult<List<GenderResponse>>> GetAllGenders()
        {
            var genders = await _authService.GetGender();
            return genders;
        }
        [HttpPost("EmailConfirmation")]
         public async Task<ActionResult<EmailConfirmationResponse>> GetAllGenders(string emailActivationCode)
        {
            var userConfirmation = await _authService.UsersEmailConfirmation(emailActivationCode);
            return userConfirmation;
        }

    }
}