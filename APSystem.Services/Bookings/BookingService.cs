using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using APSystem.Data.Entities;
using APSystem.Data.Model;
using APSystem.Data.Repositories.BookingAppointment;
using APSystem.Models.Appointment;
using APSystem.Models.Bookings;
using APSystem.Models.Email;
using APSystem.Services.Email;
using Microsoft.AspNetCore.Hosting;

namespace APSystem.Services.Bookings
{
    public class BookingService : IBookingsService
    {
        private IBookingsRepository _bookingRepository;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public BookingService(IBookingsRepository bookingRepository, IEmailService emailService, IWebHostEnvironment hostingEnvironment)
        {
            _bookingRepository = bookingRepository;
            _emailService = emailService;
            _hostingEnvironment = hostingEnvironment;
        }
        async Task<List<BookingAppointment>> IBookingsService.GetAllBookings()
        {
            var bookinglistfromDb = await _bookingRepository.GetAllBookings();
            List<BookingAppointment> listofbookings = new List<BookingAppointment>();
            foreach (BookingsModel ba in bookinglistfromDb)
            {
                listofbookings.Add(new BookingAppointment()
                {
                    BookingID = ba.BookingID,
                    PatientID = ba.PatientID,
                    PatientName = ba.PatientName,
                    DoctorID = ba.DoctorID,
                    DoctorName = ba.DoctorName,
                    AppointmentID = ba.AppointmentID,
                    AppointmentDate = ba.AppointmentDate,
                    AppointmentStartTime = ba.AppointmentStartTime,
                    AppointmentEndTime = ba.AppointmentEndTime,
                    AppointmentTypeID = ba.AppointmentTypeID,
                    AppointmentType = ba.AppointmentType,
                    PhoneNumber = ba.PhoneNumber,
                    ProblemDiscription = ba.ProblemDiscription,
                    StatusID = ba.StatusID,
                    ApStatus = ba.ApStatus
                });
            }
            return await Task.FromResult(listofbookings);
        }

        async Task<BookingAppointment> IBookingsService.CreateBooking(BookingAppointment cresteBooking)
        {
            BookingsModel bookingtodb = new BookingsModel()
            {
                PatientID = cresteBooking.PatientID,
                DoctorID = cresteBooking.DoctorID,
                AppointmentID = cresteBooking.AppointmentID,
                AppointmentTypeID = cresteBooking.AppointmentTypeID,
                PhoneNumber = cresteBooking.PhoneNumber,
                ProblemDiscription = cresteBooking.ProblemDiscription,
            };
            var create = await _bookingRepository.CreateBooking(bookingtodb);
            if(create != null){
                foreach(UserModel u in create){
                    if (u.RoleID == 2){
                       await SendDoctorEmail(u.Email);
                    }
                    else if ( u.RoleID == 1 || u.RoleID == 4 || u.RoleID == 2 || u.RoleID == 3 ){
                       await SendPatientEmail(u.Email);
                    }
                };
            }
            return await Task.FromResult(cresteBooking);
        }

        public async Task SendDoctorEmail(string doctorEmail)
        {
            string mailBody = string.Empty;
            var fileStream = new FileStream(Path.Combine(_hostingEnvironment.ContentRootPath, "EmailContent/doctor-new-booking.html"), FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                mailBody = streamReader.ReadToEnd();
            }
            List<string> emails = new List<string>();
            emails.Add(doctorEmail);
            EmailRequest emailRequest = new EmailRequest(
                         emails,
                         $"New Appointment Booking",
                        mailBody,
                         null
                     );
            await _emailService.SendEmailAsync(emailRequest);
        }
        public async Task SendPatientEmail(string patientEmail)
        {
            string mailBody = string.Empty;
            var fileStream = new FileStream(Path.Combine(_hostingEnvironment.ContentRootPath, "EmailContent/patient-new-booking.html"), FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                mailBody = streamReader.ReadToEnd();
            }
            List<string> emails = new List<string>();
            emails.Add(patientEmail);
            EmailRequest emailRequest = new EmailRequest(
                         emails,
                         $"Appointment Bookikng Email",
                        mailBody,
                         null
                     );
            await _emailService.SendEmailAsync(emailRequest);
        }


        async Task<BookingAppointment> IBookingsService.GetBookingsById(BookingAppointment bookingid)
        {
            BookingsModel bookingsbyid = new BookingsModel()
            {
                BookingID = bookingid.BookingID
            };
            var ba = await _bookingRepository.GetBookingsById(bookingsbyid);
            var bookingappointment = new BookingAppointment()
            {
                BookingID = ba.BookingID,
                PatientID = ba.PatientID,
                PatientName = ba.PatientName,
                DoctorID = ba.DoctorID,
                DoctorName = ba.DoctorName,
                AppointmentID = ba.AppointmentID,
                AppointmentDate = ba.AppointmentDate,
                AppointmentStartTime = ba.AppointmentStartTime,
                AppointmentEndTime = ba.AppointmentEndTime,
                AppointmentTypeID = ba.AppointmentTypeID,
                AppointmentType = ba.AppointmentType,
                PhoneNumber = ba.PhoneNumber,
                ProblemDiscription = ba.ProblemDiscription,
                StatusID = ba.StatusID,
                ApStatus = ba.ApStatus
            };
            return await Task.FromResult(bookingappointment);
        }

        async Task<List<BookingAppointment>> IBookingsService.GetBookingsByUserId(BookingAppointment userid)
        {
            var bookingappointment = new List<BookingAppointment>();
            BookingsModel bookingsbyuserid = new BookingsModel()
            {
                PatientID = userid.PatientID
            };
            var booking = await _bookingRepository.GetBookingsByUserId(bookingsbyuserid);
            foreach (BookingsModel ba in booking)
            {

                bookingappointment.Add(new BookingAppointment()
                {
                    BookingID = ba.BookingID,
                    PatientID = ba.PatientID,
                    PatientName = ba.PatientName,
                    DoctorID = ba.DoctorID,
                    DoctorName = ba.DoctorName,
                    AppointmentID = ba.AppointmentID,
                    AppointmentDate = ba.AppointmentDate,
                    AppointmentStartTime = ba.AppointmentStartTime,
                    AppointmentEndTime = ba.AppointmentEndTime,
                    AppointmentTypeID = ba.AppointmentTypeID,
                    AppointmentType = ba.AppointmentType,
                    PhoneNumber = ba.PhoneNumber,
                    ProblemDiscription = ba.ProblemDiscription,
                    StatusID = ba.StatusID,
                    ApStatus = ba.ApStatus
                });
            };
            return await Task.FromResult(bookingappointment);
        }

        async Task<List<BookingAppointment>> IBookingsService.GetBookingsByDoctorId(BookingAppointment doctorid)
        {
            var bookingappointment = new List<BookingAppointment>();
            BookingsModel bookingsbyuserid = new BookingsModel()
            {
                DoctorID = doctorid.DoctorID
            };
            var booking = await _bookingRepository.GetBookingsByUserId(bookingsbyuserid);
            foreach (BookingsModel ba in booking)
            {

                bookingappointment.Add(new BookingAppointment()
                {
                    BookingID = ba.BookingID,
                    PatientID = ba.PatientID,
                    PatientName = ba.PatientName,
                    DoctorID = ba.DoctorID,
                    DoctorName = ba.DoctorName,
                    AppointmentID = ba.AppointmentID,
                    AppointmentDate = ba.AppointmentDate,
                    AppointmentStartTime = ba.AppointmentStartTime,
                    AppointmentEndTime = ba.AppointmentEndTime,
                    AppointmentTypeID = ba.AppointmentTypeID,
                    AppointmentType = ba.AppointmentType,
                    PhoneNumber = ba.PhoneNumber,
                    ProblemDiscription = ba.ProblemDiscription,
                    StatusID = ba.StatusID,
                    ApStatus = ba.ApStatus
                });
            };
            return await Task.FromResult(bookingappointment);
        }

        async Task<BookingAppointment> IBookingsService.UpdateBooking(int id, BookingAppointment bookingid)
        {
            BookingsDbEntity updatebooking = new BookingsDbEntity()
            {
                AppointmentID = bookingid.AppointmentID,
                ProblemDiscription = bookingid.ProblemDiscription,
                StatusID = bookingid.StatusID
            };
            var upbooking = await _bookingRepository.UpdateBooking(id, updatebooking);
            return await Task.FromResult(bookingid);

        }
        async void IBookingsService.DeleteBooking(int id)
        {
            BookingsDbEntity deletebyid = new BookingsDbEntity()
            {
                BookingID = id
            };
            await _bookingRepository.DeleteBooking(deletebyid);
        }

        async Task<List<AppointmentType>> IBookingsService.GetAllAppointmentType()
        {
           List<AppointmentType> AptypesSrvice = new List<AppointmentType>();
           var Aptypes = await _bookingRepository.GetAllApTypes();
            foreach(var Ap in Aptypes){
                AptypesSrvice.Add(new AppointmentType{
                    AppointmentTypeID = Ap.AppointmentTypeID,
                    AppointmentTypes = Ap.AppointmentTypes
                });
            }
            return await Task.FromResult(AptypesSrvice);
        }
    }
}