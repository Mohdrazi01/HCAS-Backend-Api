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
        [Route("login")]
        public ActionResult<AuthResponse> Login([FromBody] AuthRequest request)
        {
           //_Logger.LogInformation($"Login Request:-{JsonConvert.SerializeObject(request)}");
            var response = _authService.Login(request);
            return Ok(response);
        }
    }
}