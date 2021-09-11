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
using APSystem.Models.Sms;
using APSystem.Services.Email;
using APSystem.Services.Sms;
using Microsoft.AspNetCore.Hosting;

namespace APSystem.Services.Bookings
{
    public class BookingService : IBookingsService
    {
        private readonly ISmsService _smsService;
        private IBookingsRepository _bookingRepository;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public BookingService(IBookingsRepository bookingRepository
        , IEmailService emailService
        , IWebHostEnvironment hostingEnvironment
        ,ISmsService smsService)
        {
            _bookingRepository = bookingRepository;
            _emailService = emailService;
            _hostingEnvironment = hostingEnvironment;
            _smsService = smsService;
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
            var create = _bookingRepository.CreateBooking(bookingtodb);
           // await SengSms(cresteBooking.PhoneNumber);
            await SendBookingEmail(cresteBooking.DoctorEmail, cresteBooking.PatientEmail);
            return await Task.FromResult(cresteBooking);
        }

        public async Task SengSms(string PhoneNumber)
        { SmsRequest sms = new SmsRequest(){
            To = PhoneNumber,
            From = "(240) 392-6894",
            Message = "Your booking was suffessful, Thank you From HCAS"
            };
            await _smsService.SendSmsAsync(sms);
        }


        public async Task SendBookingEmail(string doctorEmail, string patientEmail)
        {
            await SendPatientEmail(patientEmail);
            string mailBody = string.Empty;
            var fileStreamDoctor = new FileStream(Path.Combine(_hostingEnvironment.ContentRootPath, "EmailContent/doctor-new-booking.html"), FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStreamDoctor, Encoding.UTF8))
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
            var fileStreamPatient = new FileStream(Path.Combine(_hostingEnvironment.ContentRootPath, "EmailContent/patient-new-booking.html"), FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStreamPatient, Encoding.UTF8))
            {
                mailBody = streamReader.ReadToEnd();
            }
            List<string> emails = new List<string>();
            emails.Add(patientEmail);
            EmailRequest emailRequest = new EmailRequest(
                         emails,
                         $"Appointment Booking Email",
                        mailBody,
                         null
                     );
            await _emailService.SendEmailAsync(emailRequest);
        }


        async Task<BookingAppointment> IBookingsService.GetBookingsById(int bookingid)
        {
            var ba = await _bookingRepository.GetBookingsById(bookingid);
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

        async Task<List<BookingAppointment>> IBookingsService.GetBookingsByUserId(int userid)
        {
            var bookingappointment = new List<BookingAppointment>();
            var booking = await _bookingRepository.GetBookingsByUserId(userid);
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

        async Task<List<BookingAppointment>> IBookingsService.GetBookingsByDoctorId(int doctorid)
        {
            var bookingappointment = new List<BookingAppointment>();
            var booking = await _bookingRepository.GetBookingsByDoctorId(doctorid);
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
             string problem = "";
            if (bookingid.StatusID == 1)
            {
                problem = (bookingid.ProblemDiscription + " Updated on:" + DateTime.Now).ToString();
            }else{
             problem = bookingid.ProblemDiscription;
            }
            BookingsDbEntity updatebooking = new BookingsDbEntity()
            {
                BookingID = bookingid.BookingID,
                AppointmentID = bookingid.AppointmentID,
                PhoneNumber = bookingid.PhoneNumber,
                ProblemDiscription = problem,
                StatusID = bookingid.StatusID
            };
            var upbooking = await _bookingRepository.UpdateBooking(id, updatebooking);
            return await Task.FromResult(bookingid);

        }
        async void IBookingsService.DeleteBooking(int id)
        {
            await _bookingRepository.DeleteBooking(id);
        }

        async Task<List<AppointmentTypes>> IBookingsService.GetAllAppointmentType()
        {
            List<AppointmentTypes> AptypesSrvice = new List<AppointmentTypes>();
            var Aptypes = await _bookingRepository.GetAllApTypes();
            foreach (var Ap in Aptypes)
            {
                AptypesSrvice.Add(new AppointmentTypes
                {
                    AppointmentTypeID = Ap.AppointmentTypeID,
                    AppointmentType = Ap.AppointmentType
                });
            }
            return await Task.FromResult(AptypesSrvice);
        }

        async Task<List<AppointmentStatusRequest>> IBookingsService.GetApStatus()
        {
            List<AppointmentStatusRequest> apstatusreq = new List<AppointmentStatusRequest>();
            var status = await _bookingRepository.GetApStatus();
            foreach (var ap in status)
            {
                apstatusreq.Add(new AppointmentStatusRequest
                {
                    ApStatusID = ap.ApStatusID,
                    ApStatus = ap.ApStatus
                });
            }
            return await Task.FromResult(apstatusreq);

        }
    }
}