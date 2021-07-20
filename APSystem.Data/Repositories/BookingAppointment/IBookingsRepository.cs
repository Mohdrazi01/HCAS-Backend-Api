using System.Collections.Generic;
using System.Threading.Tasks;
using APSystem.Data.Entities;
using APSystem.Data.Model;

namespace APSystem.Data.Repositories.BookingAppointment
{
    public interface IBookingsRepository
    {
         Task<List<BookingsModel>> GetAllBookings();

         Task<BookingsModel> CreateBooking(BookingsModel bookings);

         Task<BookingsModel> GetBookingsById(BookingsModel bookingid);

         Task<List<BookingsModel>> GetBookingsByUserId(BookingsModel userid);

        Task<List<BookingsModel>> GetBookingsByDoctorId(BookingsModel userid);

         Task<BookingsDbEntity> UpdateBooking(int id,BookingsDbEntity bookingid);

         Task<BookingsDbEntity> DeleteBooking(BookingsDbEntity deleteid);
    }
}