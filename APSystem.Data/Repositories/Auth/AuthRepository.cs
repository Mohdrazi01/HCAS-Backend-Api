using System;
using System.Linq;
using System.Threading.Tasks;
using APSystem.Data.Contexts;
using APSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace APSystem.Data.Repositories.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApDbContext _dbContext;
        public AuthRepository(ApDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        async Task<UsersDbEntity> IAuthRepository.CreateUser(UsersDbEntity usersDbEntity)
        {
            try
            {
                if (!_dbContext.ApUsers.Any(x => x.UserName == usersDbEntity.UserName))
                {
                    usersDbEntity.IsUserNameExist = false;
                    usersDbEntity.EmailActivationCode = Guid.NewGuid();
                    _dbContext.ApUsers.Add(usersDbEntity);
                    await _dbContext.SaveChangesAsync();
                    usersDbEntity.IsUserCreated = true;
                }
                else
                {
                    usersDbEntity.IsUserNameExist = true;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }


            return usersDbEntity;
        }

         Task<UsersDbEntity> IAuthRepository.GetUser(int UserID)
        {
            throw  new System.NotImplementedException();
        }

        async Task<bool> IAuthRepository.UsersEmailConfirmation(string activationCode)
        {
            throw new System.NotImplementedException();
        }
        async Task<UsersDbEntity> IAuthRepository.GetUser(string userName)
        {
           var usersDbEntity = await _dbContext.ApUsers.FirstOrDefaultAsync(x => x.UserName == userName);
           return usersDbEntity;
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