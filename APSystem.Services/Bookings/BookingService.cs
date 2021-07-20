using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APSystem.Data.Entities;
using APSystem.Data.Model;
using APSystem.Data.Repositories.BookingAppointment;
using APSystem.Models.Appointment;
using APSystem.Models.Bookings;

namespace APSystem.Services.Bookings
{
    public class BookingService : IBookingsService
    {
        private IBookingsRepository _bookingRepository;
        public BookingService(IBookingsRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
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
            return await Task.FromResult(cresteBooking);
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
    }
}