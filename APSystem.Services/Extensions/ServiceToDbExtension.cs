using APSystem.Data.Entities;
using APSystem.Models.Auth;

namespace APSystem.Services.Extensions
{
    public static class ServiceToDbExtension
    {
        public static PatientDbEntity ToPatientUserService(this RegisterUserRequest request)
        {
            if (request == null)
                return new PatientDbEntity();
            PatientDbEntity patient = new PatientDbEntity
            {
                UserName = request.Email,
                Name = request.Name,
                RoleID = 1,
                Email = request.Email,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                Address = request.Address
            };
            return patient;


        }


        public static DoctorDbEntity ToDoctorUserService(this RegisterUserRequest request)
        {
            if (request == null)
                return new DoctorDbEntity();
            DoctorDbEntity doctor= new DoctorDbEntity
            {
                UserName = request.Email,
                Name = request.Name,
                RoleID = 2,
                Email = request.Email,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                Address = request.Address
            };
            return doctor;


        }

        
    }
}