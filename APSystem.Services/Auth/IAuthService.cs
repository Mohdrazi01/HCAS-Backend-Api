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
        Task<UserDetailsResponse> UpdateUserDetails(int id,UserDetailsRequest User);
        Task<List<UserDetailsResponse>> GetAllUsersbyRole(int roleID);
        Task<List<UserDetailsResponse>> GetAllDoctorsandNurses();
        Task<List<UserDetailsResponse>> GetAllUsers();
        Task<List<RoleResponse>> GetRoles();
        Task<EmailConfirmationResponse> UsersEmailConfirmation(string activationCode);
        Task<List<GenderResponse>> GetGender();
    }
}