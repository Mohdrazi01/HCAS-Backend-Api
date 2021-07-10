using APSystem.Data.Entities;
using APSystem.Models.Auth;

namespace APSystem.Services.Extensions
{
    public static class ServiceToDbExtension
    {
        public static UsersDbEntity ToUserService(this RegisterUserRequest request)
        {
            if (request == null)
                return new UsersDbEntity();
           UsersDbEntity Users = new UsersDbEntity
            {
                UserName = request.Email,
                Name = request.Name,
                RoleID = request.RoleID,
                Email = request.Email,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                GMCNumber = request.GMCNumber,
                Speciality = request.Speciality,
                Experience = request.Experience,
                Address = request.Address
            };
            return Users;
        }

    }
}