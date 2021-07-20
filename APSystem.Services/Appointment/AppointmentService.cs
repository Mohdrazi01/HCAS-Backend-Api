using System.ComponentModel;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using APSystem.Data.Entities;
using APSystem.Data.Repositories.Appointment;
using APSystem.Models.Appointment;
using APSystem.Services.Extensions;
using APSystem.Data.Model;

namespace APSystem.Services.Appointment
{
    public class AppointmentService : IAppointmentService
    {
        private IAppointmentRepository _appointmentRepository;
        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        async Task<AppointmentSlots> IAppointmentService.AddApSlots(AppointmentSlots apslots)
        {
            ApSlotsDbEntity apslotsdbentity = new ApSlotsDbEntity()
            {
                AppointmentStartTime = apslots.AppointmentStartTime,
                AppointmentEndTime = apslots.AppointmentEndTime,
                IsActive = true,
                CreatedBy = 1,
                CreatedDate = DateTime.Now
            };
            await _appointmentRepository.AddAppointmentSlots(apslotsdbentity);

            return await Task.FromResult(apslots);
        }

        async Task<AppointmentSlots> IAppointmentService.UpdateApSlot(AppointmentSlots updateApSlot)
        {
            ApSlotsModelItem apSlotsDb = new ApSlotsModelItem()
            {
                AppointmentSlotID = updateApSlot.AppointmentSlotID,
                AppointmentStartTime = updateApSlot.AppointmentStartTime,
                AppointmentEndTime = updateApSlot.AppointmentEndTime
            };
            await _appointmentRepository.UpdateAppointmentSlots(apSlotsDb);
            return await Task.FromResult(updateApSlot);
        }
        async Task<List<AppointmentSlots>> IAppointmentService.AllApSlots()
        {
            List<AppointmentSlots> appointmentSlots = new List<AppointmentSlots>();
            var apSlots = await _appointmentRepository.GetApSlots();
            foreach (ApSlotsModelItem a in apSlots)
            {
                appointmentSlots.Add(new AppointmentSlots()
                {
                    AppointmentSlotID = a.AppointmentSlotID,
                    AppointmentStartTime = a.AppointmentStartTime,
                    AppointmentEndTime = a.AppointmentEndTime
                });
            }
            return await Task.FromResult(appointmentSlots);
        }

        void IAppointmentService.DeleteApSlots(int id)
        {
            _appointmentRepository.DeleteAppointmentSlots(id);
        }

        async Task<AppointmentSlots> IAppointmentService.ApSlotbyId(AppointmentSlots apSlotId)
        {

            AppointmentSlots apSlots = new AppointmentSlots();
            ApSlotsModelItem apdbentity = new ApSlotsModelItem()
            {
                AppointmentSlotID = apSlotId.AppointmentSlotID
            };
            var apSlot = await _appointmentRepository.GetApSlotbyid(apdbentity);
            apSlots = new AppointmentSlots()
            {
                AppointmentSlotID = apSlot.AppointmentSlotID,
                AppointmentStartTime = apSlot.AppointmentStartTime,
                AppointmentEndTime = apSlot.AppointmentEndTime
            };
            return await Task.FromResult(apSlots);
        }

        async Task<Appointments> IAppointmentService.CreateAppointments(Appointments aps)
        {
            Appointments serviceAp = new Appointments();
            var appointments = aps.CreateAppointmentService();
            var ap = _appointmentRepository.CreateAppointments(appointments);
            return await Task.FromResult(serviceAp);
        }

        async Task<List<AppointmentwithSlotsjoin>> IAppointmentService.ListAppointments()
        {
            List<AppointmentwithSlotsjoin> appointments = new List<AppointmentwithSlotsjoin>();
            var listofap = await _appointmentRepository.ListofAppointments();
            foreach (AppointmentsModelItem ad in listofap)
            {
                appointments.Add(new AppointmentwithSlotsjoin()
                {
                    AppointmentID = ad.AppointmentID,
                    DoctorID = ad.DoctorID,
                    AppointmentDate = ad.AppointmentDate,
                    AppointmentStartTime = ad.AppointmentStartTime,
                    AppointmentEndTime = ad.AppointmentEndTime
                });
            }
            return await Task.FromResult(appointments);
        }

        async Task<List<AppointmentwithSlotsjoin>> IAppointmentService.AppointmentbyDocId(Appointments docid)
        {
            AppointmentsModelItem docidtodb = new AppointmentsModelItem() { DoctorID = docid.DoctorID };
            List<AppointmentwithSlotsjoin> _apbydocid = new List<AppointmentwithSlotsjoin>();
            var apbydocid = await _appointmentRepository.GetAppointmentsbyDocId(docidtodb);
            foreach(AppointmentsModelItem ad in apbydocid)
            {
                _apbydocid.Add(new AppointmentwithSlotsjoin()
                {
                    AppointmentID = ad.AppointmentID,
                    DoctorID = ad.DoctorID,
                    AppointmentDate = ad.AppointmentDate,
                    AppointmentStartTime = ad.AppointmentStartTime,
                    AppointmentEndTime = ad.AppointmentEndTime
                });
            }
            return await Task.FromResult(_apbydocid);
        }

        async Task<AppointmentwithSlotsjoin> IAppointmentService.AppointmentById(Appointments appointmetnID)
        {
            AppointmentsModelItem idtodb = new AppointmentsModelItem() { AppointmentID = appointmetnID.AppointmentID };
            var apbyid = await _appointmentRepository.GetAppointmentsById(idtodb);
            AppointmentwithSlotsjoin _apbyid = new AppointmentwithSlotsjoin()
            {
                AppointmentID = apbyid.AppointmentID,
                DoctorID = apbyid.DoctorID,
                AppointmentDate = apbyid.AppointmentDate,
                AppointmentStartTime = apbyid.AppointmentStartTime,
                AppointmentEndTime = apbyid.AppointmentEndTime
            };
            return await Task.FromResult(_apbyid);
        }

        async Task<Appointments> IAppointmentService.UpdateApService(Appointments updateApService)
        {

            AppointmentsDbEntity apdbtorepo = new AppointmentsDbEntity()
            {
                AppointmentID = updateApService.AppointmentID,
                AppointmentDate = updateApService.AppointmentDate,
                AppointmentTimeSlots = updateApService.AppointmentTimeSlots
            };
            await _appointmentRepository.UpdateAppointment(apdbtorepo);
            return await Task.FromResult(updateApService);
        }
        void IAppointmentService.DeleteApService(int id)
        {
              _appointmentRepository.DeleteAppointment(id);
        }
    }
}