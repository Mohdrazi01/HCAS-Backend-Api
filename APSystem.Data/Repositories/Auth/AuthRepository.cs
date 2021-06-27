using System;
using System.Linq;
using System.Threading.Tasks;
using APSystem.Data.Contexts;
using APSystem.Data.Entities;

namespace APSystem.Data.Repositories.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApDbContext _dbContext;
        public AuthRepository(ApDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        async Task<DoctorDbEntity> IAuthRepository.CreateDoctorUser(DoctorDbEntity doctorDbEntity)
        {
            throw new System.NotImplementedException();
        }

        async Task<PatientDbEntity> IAuthRepository.CreatePatientUser(PatientDbEntity patientDbEntity)
        {
            try
            {
                if (!_dbContext.Patients.Any(x => x.UserName == patientDbEntity.UserName))
                {
                    patientDbEntity.IsUserNameExist = false;
                    patientDbEntity.EmailActivationCode = Guid.NewGuid();
                    _dbContext.Patients.Add(patientDbEntity);
                    await _dbContext.SaveChangesAsync();
                    patientDbEntity.IsUserCreated = true;
                }
                else
                {
                    patientDbEntity.IsUserNameExist = true;
                }
            }
            catch (System.Exception ex)
            {
                // TODO
            }


            return patientDbEntity;
        }

        async Task<bool> IAuthRepository.DoctorEmailConfirmation(string activationCode)
        {
            throw new System.NotImplementedException();
        }

        async Task<DoctorDbEntity> IAuthRepository.GetDoctorUser(int doctorUserID)
        {
            throw new System.NotImplementedException();
        }

        async Task<PatientDbEntity> IAuthRepository.GetPatientUser(int patientUserID)
        {
            throw new System.NotImplementedException();
        }

        async Task<bool> IAuthRepository.PatientEmailConfirmation(string activationCode)
        {
            throw new System.NotImplementedException();
        }

        // async Task<long> IAuthRepository.SaveDoctorLoginHistory(DoctorLoginHistoryDbEntity doctorLoginHistoryDbEntity)
        // {
        //     throw new System.NotImplementedException();
        // }

        //async  Task<long> IAuthRepository.SavePatientLoginHistory(PatientLoginHistoryDbEntity patientLoginHistoryDbEntity)
        // {
        //     throw new System.NotImplementedException();
        // }
    }
}