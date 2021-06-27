using System.Threading.Tasks;
using APSystem.Models.Auth;

namespace APSystem.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);
        Task<RegisterUserResponse> RegisterUser(RegisterUserRequest request);
    }
}