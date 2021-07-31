using System.Collections.Generic;
using System.Threading.Tasks;
using APSystem.Models.Auth;

namespace APSystem.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);
        Task<RegisterUserResponse> RegisterUser(RegisterUserRequest request);
         Task<UserDetailsResponse> GetUser(int UserID);
        Task<List<UserDetailsResponse>> GetAllUsersbyRole(int roleID);
        Task<List<UserDetailsResponse>> GetAllUsers();
        Task<List<RoleResponse>> GetRoles();
        Task<EmailConfirmationResponse> UsersEmailConfirmation(string activationCode);
        Task<List<GenderResponse>> GetGender();
    }
}