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
        Task<List<AppointmentTypes>> GetAllAppointmentType();
        Task<BookingAppointment> GetBookingsById(int bookingid);
        Task<List<BookingAppointment>> GetBookingsByUserId(int patientid);
        Task<List<BookingAppointment>> GetBookingsByDoctorId(int doctorid);

         Task<List<AppointmentStatusRequest>> GetApStatus();
        Task<BookingAppointment> UpdateBooking(int id, BookingAppointment bookingid);
        void DeleteBooking(int id);
    }
}