using System;
using APSystem.Data.Entities;
using APSystem.Data.Model;
using APSystem.Models.Appointment;
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

        public static AppointmentsModelItem CreateAppointmentService(this Appointments Aps)
        {
            AppointmentsModelItem apDbEntity = new AppointmentsModelItem(){
                DoctorID = Aps.DoctorID,
                AppointmentDate = Aps.AppointmentDate,
                AppointmentTimeSlots = Aps.AppointmentTimeSlotsArray,
                IsActive = true,
                CreatedBy = 1,
                CreatedDate = DateTime.Now,
                ModifiedBy = 0,
                ModifiedDate =  DateTime.Now
            };

            return apDbEntity;
        }

    }
}