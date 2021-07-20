using System.Threading.Tasks;
using APSystem.Data.Entities;

namespace APSystem.Data.Repositories.Auth
{
    public interface IAuthRepository
    {
        Task<UsersDbEntity> CreateUser(UsersDbEntity usersDbEntity);
        Task<UsersDbEntity> GetUser(int UserID);

        Task<UsersDbEntity> GetUser(string userName);
        Task<bool> UsersEmailConfirmation(string activationCode);
        

        // Task<long> SavePatientLoginHistory(PatientLoginHistoryDbEntity patientLoginHistoryDbEntity);
        // Task<long> SaveDoctorLoginHistory(DoctorLoginHistoryDbEntity doctorLoginHistoryDbEntity);
    }
}