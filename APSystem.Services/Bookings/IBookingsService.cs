using System.Collections.Generic;
using System.Threading.Tasks;
using APSystem.Models.Appointment;
using APSystem.Models.Bookings;

namespace APSystem.Services.Bookings
{
    public interface IBookingsService
    {
        Task<List<BookingAppointment>> GetAllBookings();
        Task<BookingAppointment> CreateBooking(BookingAppointment createBooking);
        Task<BookingAppointment> GetBookingsById(BookingAppointment bookingid);
        Task<List<BookingAppointment>> GetBookingsByUserId(BookingAppointment patientid);
        Task<List<BookingAppointment>> GetBookingsByDoctorId(BookingAppointment doctorid);
        Task<BookingAppointment> UpdateBooking(int id, BookingAppointment bookingid);
        void DeleteBooking(int id);
    }
}