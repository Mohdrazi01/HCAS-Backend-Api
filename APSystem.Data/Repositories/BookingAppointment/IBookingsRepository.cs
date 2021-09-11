using System.Collections.Generic;
using System.Threading.Tasks;
using APSystem.Data.Entities;
using APSystem.Data.Model;

namespace APSystem.Data.Repositories.BookingAppointment
{
    public interface IBookingsRepository
    {
         Task<List<BookingsModel>> GetAllBookings();

         Task<List<UserModel>> CreateBooking(BookingsModel bookings);
         Task<List<AppointmentTypeModel>> GetAllApTypes();

         Task<BookingsModel> GetBookingsById(int bookingid);

         Task<List<BookingsModel>> GetBookingsByUserId(int userid);

        Task<List<BookingsModel>> GetBookingsByDoctorId(int userid);

         Task<List<AppointmentStatus>> GetApStatus();

         Task<BookingsDbEntity> UpdateBooking(int id,BookingsDbEntity bookingid);

         Task<BookingsDbEntity> DeleteBooking(int deleteid);
    }
}