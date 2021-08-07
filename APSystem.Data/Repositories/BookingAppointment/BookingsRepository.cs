using System.Xml.Linq;
using System.Runtime.CompilerServices;
using System.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APSystem.Data.Contexts;
using APSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using APSystem.Configuration.Settings;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using APSystem.Data.Model;

namespace APSystem.Data.Repositories.BookingAppointment
{
    public class BookingsRepository : IBookingsRepository
    {
        private ApDbContext _dbContext;
        private IOptions<ConnectionSettings> _connectionSetting;
        public BookingsRepository(ApDbContext dbContext, IOptions<ConnectionSettings> connectionSetting)
        {
            _dbContext = dbContext;
            _connectionSetting = connectionSetting;
        }

        async Task<List<BookingsModel>> IBookingsRepository.GetAllBookings()
        {
            var listofBookings = new List<BookingsModel>();
            try
            {
                using (IDbConnection con = new SqlConnection(_connectionSetting.Value.DefaultConnection))
                {
                    string spname = "GetAllBookings";
                    var listDto = con.Query<BookingsModel>(spname, commandType: CommandType.StoredProcedure, commandTimeout: 1000).ToList();
                    listofBookings.AddRange(listDto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(listofBookings);
        }
        async Task<List<UserModel>> IBookingsRepository.CreateBooking(BookingsModel bookingsModel)
        {
            List<UserModel> users = new List<UserModel>();
            try
            {
                UserModel Doctor = new UserModel()
                {
                    UserID = bookingsModel.DoctorID
                };
                UserModel Patient = new UserModel()
                {
                    UserID = bookingsModel.PatientID
                };

                using (IDbConnection con = new SqlConnection(_connectionSetting.Value.DefaultConnection))
                {
                    string spname = "CreateBooking";
                    var parameters = new DynamicParameters();
                    parameters.Add("@PatientID", bookingsModel.PatientID, DbType.Int32);
                    parameters.Add("@DoctorID", bookingsModel.DoctorID, DbType.Int32);
                    parameters.Add("@AppointmentID", bookingsModel.AppointmentID, DbType.Int32);
                    parameters.Add("@AppointmentTypeID", bookingsModel.AppointmentTypeID, DbType.Int32);
                    parameters.Add("@PhoneNumber", bookingsModel.PhoneNumber, DbType.String);
                    parameters.Add("@ProblemDiscription", bookingsModel.ProblemDiscription, DbType.String);
                    var bookingSuccess = await con.ExecuteAsync(spname, parameters, commandType: CommandType.StoredProcedure, commandTimeout: 1000);
                    var doctor = await _dbContext.ApUsers.SingleOrDefaultAsync(x => x.UserID == Doctor.UserID);
                    var patient = await _dbContext.ApUsers.SingleOrDefaultAsync(x => x.UserID == Patient.UserID);
                    if (doctor != null && patient != null)
                    {
                        UserModel Doctor1 = new UserModel()
                        {
                            UserID = doctor.UserID,
                            RoleID = doctor.RoleID,
                            Email = doctor.Email,
                            Name = doctor.Name
                        };
                        UserModel Patient1 = new UserModel()
                        {
                            UserID = patient.UserID,
                            RoleID = patient.RoleID,
                            Email = patient.Email,
                            Name = patient.Email
                        };
                        users.Add(Doctor1);
                        users.Add(Patient1);
                    }

                }
            }
            catch (DBConcurrencyException)
            {
                throw new Exception();
            }
            return await Task.FromResult(users);
        }


        async Task<BookingsModel> IBookingsRepository.GetBookingsById(BookingsModel bookingbyId)
        {
            BookingsModel bookingbyid = new BookingsModel();
            using (IDbConnection con = new SqlConnection(_connectionSetting.Value.DefaultConnection))
            {
                string spname = "GetBookingsbyID";
                var parameters = new DynamicParameters();
                parameters.Add("@id", bookingbyId.AppointmentID, DbType.Int32);
                var bookingbyidDto = con.QuerySingleOrDefault(spname, parameters, commandType: CommandType.StoredProcedure, commandTimeout: 1000);
                bookingbyid = bookingbyidDto;
            }
            return await Task.FromResult(bookingbyid);
        }

        async Task<List<BookingsModel>> IBookingsRepository.GetBookingsByUserId(BookingsModel userid)
        {
            var finduserbooking = new List<BookingsModel>();
            try
            {
                using (IDbConnection con = new SqlConnection(_connectionSetting.Value.DefaultConnection))
                {
                    string spname = "GetAllBookingsbyPatientID";
                    var parameters = new DynamicParameters();
                    parameters.Add("@PatientId", userid.PatientID, DbType.Int32);
                    var listofpatientbookingsDto = con.Query<BookingsModel>(spname, parameters, commandType: CommandType.StoredProcedure, commandTimeout: 1000).ToList();
                    finduserbooking.AddRange(listofpatientbookingsDto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(finduserbooking);
        }

        async Task<List<BookingsModel>> IBookingsRepository.GetBookingsByDoctorId(BookingsModel doctorid)
        {
            string Message = "";
            var finddocbooking = new List<BookingsModel>();
            try
            {
                using (IDbConnection con = new SqlConnection(_connectionSetting.Value.DefaultConnection))
                {
                    string spname = "GetAllBookingsbyDoctorID";
                    var parameters = new DynamicParameters();
                    parameters.Add("@DoctotID", doctorid.DoctorID, DbType.Int32);
                    var listofbookingbyDridDto = con.Query<BookingsModel>(spname, parameters, commandType: CommandType.StoredProcedure, commandTimeout: 1000).ToList();
                    finddocbooking.AddRange(listofbookingbyDridDto);
                }
            }
            catch (Exception ex)
            {
                Message = ex.ToString();
            }
            return await Task.FromResult(finddocbooking);
        }

        async Task<BookingsDbEntity> IBookingsRepository.UpdateBooking(int id, BookingsDbEntity updatebooking)
        {
            BookingsModel boookingmodel = new BookingsModel()
            {
                BookingID = id,
                AppointmentID = updatebooking.AppointmentID,
                PhoneNumber = updatebooking.PhoneNumber,
                ProblemDiscription = updatebooking.ProblemDiscription,
                StatusID = updatebooking.StatusID
            };
            try
            {
                // var existingbooking = await _dbContext.Bookings.FindAsync(id);
                // if (existingbooking != null)
                // {
                //     _dbContext.Bookings.Update(updatebooking);
                //     _dbContext.SaveChanges();
                // }
                using (IDbConnection con = new SqlConnection(_connectionSetting.Value.DefaultConnection))
                {
                    string spname = "EditbookingbyID";
                    var parameters = new DynamicParameters();
                    parameters.Add("@BookingID", boookingmodel.BookingID);
                    parameters.Add("@AppointmentID", boookingmodel.AppointmentID);
                    parameters.Add("@PhoneNumber", boookingmodel.PhoneNumber);
                    parameters.Add("@ProblemDiscription", boookingmodel.ProblemDiscription);
                    parameters.Add("@StatusID", boookingmodel.StatusID);
                    var updatedbooking = await con.ExecuteAsync(spname, parameters);
                }


            }
            catch (Exception)
            {
                throw new Exception();
            }
            return await Task.FromResult(updatebooking);
        }
        async Task<BookingsDbEntity> IBookingsRepository.DeleteBooking(BookingsDbEntity deleteid)
        {
            try
            {
                var deleteBookings = await _dbContext.Bookings.FindAsync(deleteid.BookingID);
                if (deleteBookings != null)
                {
                    _dbContext.Bookings.Remove(deleteBookings);
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
            return await Task.FromResult(deleteid);
        }

        async Task<List<AppointmentTypeModel>> IBookingsRepository.GetAllApTypes()
        {
            List<AppointmentTypeModel> Aptypes = new List<AppointmentTypeModel>();
            using(IDbConnection con = new SqlConnection(_connectionSetting.Value.DefaultConnection))
            {
              string spname = "GetAllAppointmentType";
              var AptypesList = con.Query<AppointmentTypeModel>(spname,commandType: CommandType.StoredProcedure, commandTimeout: 1000).ToList();
              Aptypes.AddRange(AptypesList);
            }
            return await Task.FromResult(Aptypes);
        }
    }
}