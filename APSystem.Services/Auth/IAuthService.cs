using APSystem.Models.Auth;

namespace APSystem.Services.Auth
{
    public interface IAuthService
    {
        AuthResponse Login(AuthRequest request);
    }
}