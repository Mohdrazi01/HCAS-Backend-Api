using APSystem.Models.Auth;
using APSystem.Services.MetaData;
using Microsoft.Extensions.Logging;
namespace APSystem.Services
{
    public class BaseService<T> : IBaseService
    {
        private readonly ILogger<T> _logger;
        //private readonly IConfiguration _config;
        public ILogger<T> _Logger { get { return _logger; } }
        IMetaDataService _metaDataService;
        public BaseService(IMetaDataService metaDataService,ILogger<T> logger)
        {
            _metaDataService = metaDataService;
            _logger = logger;
        }
        public IMetaDataService RequestMetaData { get { return _metaDataService; } }
        public AuthResponse AuthData { get { return _metaDataService.GetMetaData().AuthData; } }
    }
}