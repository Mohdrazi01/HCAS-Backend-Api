using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace APSystem.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        private readonly ILogger<T> _logger;
        //private readonly IConfiguration _config;
        public BaseController(ILogger<T> logger)
        {
            _logger = logger;
        }
        public ILogger<T> _Logger { get { return _logger; } }
    }
}