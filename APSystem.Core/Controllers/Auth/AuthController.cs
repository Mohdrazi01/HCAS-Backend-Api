
using System.Threading.Tasks;
using APSystem.Models.Auth;
using APSystem.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
            var response =await _authService.Login(request);
            return Ok(response);
        }

    }
}