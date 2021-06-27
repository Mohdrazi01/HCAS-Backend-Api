using System.Threading.Tasks;
using APSystem.Data.Entities;

namespace APSystem.Data.Repositories.Auth
{
    public interface IAuthRepository
    {
        Task<PatientDbEntity> CreatePatientUser(PatientDbEntity patientDbEntity);
        Task<DoctorDbEntity> CreateDoctorUser(DoctorDbEntity doctorDbEntity);
        Task<PatientDbEntity> GetPatientUser(int patientUserID);
        Task<DoctorDbEntity> GetDoctorUser(int doctorUserID);
        Task<bool> PatientEmailConfirmation(string activationCode);
        Task<bool> DoctorEmailConfirmation(string activationCode);
        // Task<long> SavePatientLoginHistory(PatientLoginHistoryDbEntity patientLoginHistoryDbEntity);
        // Task<long> SaveDoctorLoginHistory(DoctorLoginHistoryDbEntity doctorLoginHistoryDbEntity);
    }
}