using System.Collections.Generic;
using System.Threading.Tasks;
using APSystem.Data.Entities;
using APSystem.Models.Appointment;

namespace APSystem.Services.Appointment
{
    public interface IAppointmentService
    {
        Task<AppointmentSlots> AddApSlots(AppointmentSlots appointmentSlots);
        Task<List<AppointmentSlots>> AllApSlots();
        Task<AppointmentSlots> ApSlotbyId(AppointmentSlots apslotid);
        Task<AppointmentSlots> UpdateApSlot(AppointmentSlots updateApSlot);
        void DeleteApSlots(int id);
        Task<Appointments> CreateAppointments(Appointments Aps);
        Task<List<AppointmentwithSlotsjoin>> ListAppointments();
        Task<List<AppointmentwithSlotsjoin>> AppointmentbyDocId(Appointments docid);
        Task<AppointmentwithSlotsjoin> AppointmentById(Appointments appointmentID);
        Task<Appointments> UpdateApService(Appointments updateApService);
        void DeleteApService(int id);
    }
}