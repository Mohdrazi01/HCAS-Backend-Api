using APSystem.Models.Auth;
using APSystem.Services.MetaData;
using Microsoft.Extensions.Logging;

namespace APSystem.Services.Auth
{
    public class AuthService : BaseService<AuthService>,  IAuthService
    {
        public AuthService(IMetaDataService metaDataService, ILogger<AuthService> logger): base(metaDataService,logger)
        {
            
        }

        AuthResponse IAuthService.Login(AuthRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}