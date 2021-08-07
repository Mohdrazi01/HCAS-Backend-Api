using System.Collections.Generic;
using System.Threading.Tasks;
using APSystem.Data.Entities;
using APSystem.Data.Model;

namespace APSystem.Data.Repositories.Auth
{
    public interface IAuthRepository
    {
        Task<UsersDbEntity> CreateUser(UsersDbEntity usersDbEntity);
        Task<UserModel> GetUser(int UserID);
        Task<UserModel> UpdateUserDetails(int id,UserModel User);
        Task<List<UserModel>> GetAllUsersbyRole(int roleID);
        Task<List<UserModel>> GetAllUsers();
        Task<List<RolesModelItem>> GetRoles();
        Task<List<GenderModel>> GetGender();
        Task<List<UserModel>> GetAllDoctorsandNurses();
        Task<UsersDbEntity> GetUser(string userName);
        Task<bool> UsersEmailConfirmation(string activationCode);

        // Task<long> SavePatientLoginHistory(PatientLoginHistoryDbEntity patientLoginHistoryDbEntity);
        // Task<long> SaveDoctorLoginHistory(DoctorLoginHistoryDbEntity doctorLoginHistoryDbEntity);
    }
}