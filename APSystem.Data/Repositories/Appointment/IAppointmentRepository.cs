using System.Collections.Generic;
using System.Threading.Tasks;
using APSystem.Data.Entities;
using APSystem.Data.Model;

namespace APSystem.Data.Repositories.Appointment
{
    public interface IAppointmentRepository
    {
        Task<ApSlotsDbEntity> AddAppointmentSlots(ApSlotsDbEntity apslots);
        Task<List<ApSlotsModelItem>> GetApSlots();
        Task<ApSlotsModelItem> GetApSlotbyid(ApSlotsModelItem apSlotId);
        Task<ApSlotsModelItem> UpdateAppointmentSlots(ApSlotsModelItem uapslots);
        void DeleteAppointmentSlots(int id);
        Task<AppointmentsModelItem> CreateAppointments(AppointmentsModelItem aps);
        Task<List<AppointmentsModelItem>> ListofAppointments();
        Task<List<AppointmentsModelItem>> GetAppointmentsbyDocId(AppointmentsModelItem docid);

        Task<AppointmentsModelItem> GetAppointmentsById(AppointmentsModelItem appointmentID);

        Task<AppointmentsDbEntity> UpdateAppointment(AppointmentsDbEntity updateAppointment);

        void DeleteAppointment(int id);
    }
}