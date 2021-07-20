using System.Runtime.InteropServices;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APSystem.Data.Contexts;
using APSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using APSystem.Data.Model;
using Microsoft.Extensions.Options;
using APSystem.Configuration.Settings;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;

namespace APSystem.Data.Repositories.Appointment
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApDbContext _dbContext;

        private IOptions<ConnectionSettings> _connectionSetting;
        public AppointmentRepository(ApDbContext dbContext, IOptions<ConnectionSettings> connectionSetting)
        {
            _dbContext = dbContext;
            _connectionSetting = connectionSetting;
        }


        async Task<ApSlotsDbEntity> IAppointmentRepository.AddAppointmentSlots(ApSlotsDbEntity apslots)
        {
            _dbContext.ApSlots.Add(apslots);
            _dbContext.SaveChanges();
            return await Task.FromResult(apslots);
        }
        async Task<List<ApSlotsModelItem>> IAppointmentRepository.GetApSlots()
        {
            var listofApSlots = new List<ApSlotsModelItem>();
            using (IDbConnection con = new SqlConnection(_connectionSetting.Value.DefaultConnection))
            {
                string spname = "GetAllApSlots";
                var listofApSlotsdto = con.Query<ApSlotsModelItem>(spname, commandType: CommandType.StoredProcedure, commandTimeout: 1000).ToList();
                listofApSlots.AddRange(listofApSlotsdto);
            }
            return await Task.FromResult(listofApSlots);
        }

        async Task<ApSlotsModelItem> IAppointmentRepository.GetApSlotbyid(ApSlotsModelItem apSlotId)
        {
            var _apSlotbyId = new ApSlotsModelItem();
            using(IDbConnection con = new SqlConnection(_connectionSetting.Value.DefaultConnection)){
                 string spname = "GetApSlotbyID";
                 var parameters = new DynamicParameters();
                 parameters.Add("@id",apSlotId.AppointmentSlotID,DbType.Int32);
                 var apSlot = con.QuerySingleOrDefault<ApSlotsModelItem>(spname,parameters);
                 _apSlotbyId = apSlot;
            };
           return await Task.FromResult(_apSlotbyId);
        }



        async Task<ApSlotsModelItem> IAppointmentRepository.UpdateAppointmentSlots(ApSlotsModelItem updateapslot)
        {
            try
            {
                ApSlotsDbEntity updApSlot = new ApSlotsDbEntity()
                {
                    AppointmentSlotID = updateapslot.AppointmentSlotID,
                    AppointmentStartTime = updateapslot.AppointmentStartTime,
                    AppointmentEndTime = updateapslot.AppointmentEndTime
                };
                _dbContext.ApSlots.Update(updApSlot);
                await _dbContext.SaveChangesAsync();
            }
            catch (DBConcurrencyException)
            {
                throw;
            }
            return await Task.FromResult(updateapslot);
        }
        void IAppointmentRepository.DeleteAppointmentSlots(int id)
        {
            var deleteapslot = _dbContext.ApSlots.Find(id);
            if (deleteapslot != null)
            {
                _dbContext.ApSlots.Remove(deleteapslot);
                _dbContext.SaveChanges();
            };
        }
        async Task<AppointmentsDbEntity> IAppointmentRepository.CreateAppointments(AppointmentsDbEntity aps)
        {
            _dbContext.Appointments.Add(aps);
            _dbContext.SaveChanges();
            return await Task.FromResult(aps);
        }

        async Task<List<AppointmentsModelItem>> IAppointmentRepository.ListofAppointments()
        {
            List<AppointmentsModelItem> appointmentdbentity = new List<AppointmentsModelItem>();
            using (IDbConnection con = new SqlConnection(_connectionSetting.Value.DefaultConnection))
            {
                string spname = "GetAllAppointments";
                con.Open();
                var listofApDto = con.Query<AppointmentsModelItem>(spname, commandType: CommandType.StoredProcedure, commandTimeout: 1000).ToList();
                appointmentdbentity.AddRange(listofApDto);
            }
            return await Task.FromResult(appointmentdbentity);

        }

        async Task<List<AppointmentsModelItem>> IAppointmentRepository.GetAppointmentsbyDocId(AppointmentsModelItem docid)
        {
            List<AppointmentsModelItem> _apbydocid = new List<AppointmentsModelItem>();
            using (IDbConnection con = new SqlConnection(_connectionSetting.Value.DefaultConnection))
            {
                string spname = "GetAllAppointmentbydocID";
                var parameters = new DynamicParameters();
                parameters.Add("@DocId",docid.DoctorID,DbType.Int32);
                var listofApDto = con.Query<AppointmentsModelItem>(spname,parameters, commandType: CommandType.StoredProcedure,commandTimeout: 1000).ToList();
                _apbydocid.AddRange(listofApDto);

            }
            return await Task.FromResult(_apbydocid);
        }

        async Task<AppointmentsModelItem> IAppointmentRepository.GetAppointmentsById(AppointmentsModelItem appointmentID)
        {
            var _apbyid = new AppointmentsModelItem();
           using(IDbConnection con = new SqlConnection(_connectionSetting.Value.DefaultConnection)){
               string spname = "GetAllAppointmentsbyID";
               var parameters = new DynamicParameters();
               parameters.Add("@id",appointmentID.AppointmentID,DbType.Int32);
               var apbyidDto =  con.QuerySingleOrDefault<AppointmentsModelItem>(spname,parameters,commandType:CommandType.StoredProcedure,commandTimeout:1000);
               _apbyid = apbyidDto;
           }
           return await Task.FromResult(_apbyid);
        }

        async Task<AppointmentsDbEntity> IAppointmentRepository.UpdateAppointment(AppointmentsDbEntity updateAppointment)
        {
            try
            {
                _dbContext.Appointments.Update(updateAppointment);
                await _dbContext.SaveChangesAsync();
            }
            catch (DBConcurrencyException)
            {
                throw;
            }
            return await Task.FromResult(updateAppointment);
        }

        async void IAppointmentRepository.DeleteAppointment(int id)
        {

            var deleteap = await _dbContext.Appointments.FindAsync(id);
            if (deleteap != null)
            {
                _dbContext.Appointments.Remove(deleteap);
                _dbContext.SaveChanges();
            }
        }
    }

}