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
        public AuthRepository(ApDbContext dbContext, IOptions<ConnectionSettings> connectionSetting)
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
            using (IDbConnection con = new SqlConnection(_connectionSetting.Value.DefaultConnection))
            {
                string spname = "GetUsersByID";
                var parameters = new DynamicParameters();
                parameters.Add("@UserID", UserID, DbType.Int32);
                var usersDto = con.QuerySingleOrDefaultAsync<UserModel>(spname, parameters, commandType: CommandType.StoredProcedure, commandTimeout: 1000);
                UsersbyId = await usersDto;
            }
            return await Task.FromResult(UsersbyId);
        }

        async Task<bool> IAuthRepository.UsersEmailConfirmation(string activationCode)
        {
            activationCode.ToUpper();
            var user = await _dbContext.ApUsers.SingleOrDefaultAsync(x => x.EmailActivationCode.ToString()
             == activationCode);
            if (user != null)
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

        async Task<List<UserModel>> IAuthRepository.GetAllUsersbyRole(int roleID)
        {

            List<UserModel> listofUsers = new List<UserModel>();
            using (IDbConnection con = new SqlConnection(_connectionSetting.Value.DefaultConnection))
            {
                string spname = "GetUsersByRoleID";
                var parameters = new DynamicParameters();
                parameters.Add("@RoleID", roleID, DbType.Int32);
                var listusersDto = con.Query<UserModel>(spname, parameters, commandType: CommandType.StoredProcedure, commandTimeout: 1000).ToList();
                listofUsers.AddRange(listusersDto);
            }
            return await Task.FromResult(listofUsers);
        }

        async Task<List<UserModel>> IAuthRepository.GetAllDoctorsandNurses()
        {
            List<UserModel> listofUsers = new List<UserModel>();
            using (IDbConnection con = new SqlConnection(_connectionSetting.Value.DefaultConnection))
            {
                string spname = "GetAllDoctorandNurse";
                var listusersDto = con.Query<UserModel>(spname, commandType: CommandType.StoredProcedure, commandTimeout: 1000).ToList();
                listofUsers.AddRange(listusersDto);
            }
            return await Task.FromResult(listofUsers);
        }

        async Task<List<UserModel>> IAuthRepository.GetAllUsers()
        {
            List<UserModel> listofUsers = new List<UserModel>();
            using (IDbConnection con = new SqlConnection(_connectionSetting.Value.DefaultConnection))
            {
                string spname = "GetAllUsers";
                var listUsersDto = con.Query<UserModel>(spname, commandType: CommandType.StoredProcedure, commandTimeout: 1000).ToList();
                listofUsers.AddRange(listUsersDto);
            }
            return await Task.FromResult(listofUsers);
        }

        async Task<List<RolesModelItem>> IAuthRepository.GetRoles()
        {
            List<RolesModelItem> listofRoles = new List<RolesModelItem>();
            using (IDbConnection con = new SqlConnection(_connectionSetting.Value.DefaultConnection))
            {
                string spname = "GetAllRoles";
                var listRolesDto = con.Query<RolesModelItem>(spname, commandType: CommandType.StoredProcedure, commandTimeout: 1000).ToList();
                listofRoles.AddRange(listRolesDto);
            }
            return await Task.FromResult(listofRoles);
        }

        async Task<List<GenderModel>> IAuthRepository.GetGender()
        {
            List<GenderModel> listofGender = new List<GenderModel>();
            using (IDbConnection con = new SqlConnection(_connectionSetting.Value.DefaultConnection))
            {
                string spname = "GetAllGenders";
                var listGenderDto = con.Query<GenderModel>(spname, commandType: CommandType.StoredProcedure, commandTimeout: 1000).ToList();
                listofGender.AddRange(listGenderDto);
            }
            return await Task.FromResult(listofGender);
        }

        async Task<UserModel> IAuthRepository.UpdateUserDetails(int id, UserModel User)
        {
            var user = await _dbContext.ApUsers.SingleOrDefaultAsync(x => x.UserID == id);
            if (user != null)
            {
                user.Name = User.Name;
                user.RoleID = User.RoleID;
                user.Email = User.Email;
                user.PhoneNumber = User.PhoneNumber;
                user.DateOfBirth = User.DateOfBirth;
                user.Gender = User.Gender;
                user.GMCNumber = User.GMCNumber;
                user.Speciality = User.Speciality;
                user.Experience = User.Experience;
                user.Address = User.Address;

                var updateduser = _dbContext.ApUsers.Update(user);
                var result = _dbContext.SaveChanges();
            }
            return await Task.FromResult(User);
        }


    }
}