using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APSystem.Data.Contexts;
using APSystem.Data.Entities;
using APSystem.Data.Model;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;
using APSystem.Configuration.Settings;
using Dapper;

namespace APSystem.Data.Repositories.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApDbContext _dbContext;
        private IOptions<ConnectionSettings> _connectionSetting;
        public AuthRepository(ApDbContext dbContext,IOptions<ConnectionSettings> connectionSetting)
        {
            _dbContext = dbContext;
            _connectionSetting = connectionSetting;
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

        async Task<UserModel> IAuthRepository.GetUser(int UserID)
        {
             UserModel UsersbyId = new UserModel();
            using(IDbConnection con = new SqlConnection(_connectionSetting.Value.DefaultConnection)){
                string spname = "GetUsersByRoleID";
                var parameters = new DynamicParameters();
                parameters.Add("@UserID",UserID, DbType.Int32);
                var usersDto =  con.QuerySingleOrDefault(spname,parameters,commandType:CommandType.StoredProcedure,commandTimeout:1000);
                UsersbyId = usersDto;
            }
            return await Task.FromResult(UsersbyId);
        }

        async Task<bool> IAuthRepository.UsersEmailConfirmation(string activationCode)
        {
           activationCode.ToUpper();
           var user = await _dbContext.ApUsers.SingleOrDefaultAsync(x=> x.EmailActivationCode.ToString()
            == activationCode);
           if(user != null)
           {
               user.IsEmailConfirmed = true;
               _dbContext.ApUsers.Update(user);
               _dbContext.SaveChanges();
               return true;
           }
           else
           {
               return false;
           }
        }
        async Task<UsersDbEntity> IAuthRepository.GetUser(string userName)
        {
           var usersDbEntity = await _dbContext.ApUsers.FirstOrDefaultAsync(x => x.UserName == userName);
           return usersDbEntity;
        }

        public Task<List<UserModel>> GetAllUsersbyRole(int roleID)
        {

            List<UserModel> listofUsers = new List<UserModel>();
            using(IDbConnection con = new SqlConnection(_connectionSetting.Value.DefaultConnection)){
                string spname = "GetUsersByRoleID";
                var parameters = new DynamicParameters();
                parameters.Add("@RoleID",roleID, DbType.Int32);
                var listusersDto =  con.Query<UserModel>(spname,parameters,commandType:CommandType.StoredProcedure,commandTimeout:1000).ToList();
                listofUsers.AddRange(listusersDto);
            }
            return Task.FromResult(listofUsers);
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
             List<UserModel> listofUsers = new List<UserModel>();
            using(IDbConnection con = new SqlConnection(_connectionSetting.Value.DefaultConnection)){
                string spname = "GetAllUsers";
                var listUsersDto =  con.Query<UserModel>(spname, commandType:CommandType.StoredProcedure,commandTimeout:1000).ToList();
                listofUsers.AddRange(listUsersDto);
            }
            return await Task.FromResult(listofUsers);
        }

        public Task<List<RolesModelItem>> GetRoles()
        {
            List<RolesModelItem> listofRoles = new List<RolesModelItem>();
            using(IDbConnection con = new SqlConnection(_connectionSetting.Value.DefaultConnection)){
                string spname = "GetAllRoles";
                var listRolesDto =  con.Query<RolesModelItem>(spname,commandType:CommandType.StoredProcedure,commandTimeout:1000).ToList();
                listofRoles.AddRange(listRolesDto);
            }
            return Task.FromResult(listofRoles);
        }

        public Task<List<GenderModel>> GetGender()
        {
            List<GenderModel> listofGender = new List<GenderModel>();
            using(IDbConnection con = new SqlConnection(_connectionSetting.Value.DefaultConnection)){
                string spname = "GetAllGenders";
                var listGenderDto =  con.Query<GenderModel>(spname,commandType:CommandType.StoredProcedure,commandTimeout:1000).ToList();
                listofGender.AddRange(listGenderDto);
            }
            return Task.FromResult(listofGender);
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