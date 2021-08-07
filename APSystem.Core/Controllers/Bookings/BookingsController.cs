using System.Collections.Generic;
using System.Net.Cache;
using System.Threading.Tasks;
using APSystem.Models.Bookings;
using APSystem.Services.Bookings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace APSystem.Core.Controllers.Bookings
{
    [Route("api/v1/Booking")]
    [ApiController]
    public class BookingsController : BaseController<BookingsController>
    {
        private IBookingsService _bookingsService;
        public BookingsController(ILogger<BookingsController> logger, IBookingsService bookingsService) : base(logger)
        {
            _bookingsService = bookingsService;
        }
        [HttpGet("GetAllBookings")]
        public async Task<ActionResult<List<BookingAppointment>>> GetAllBookings()
        {

            var bookings = await _bookingsService.GetAllBookings();
            return bookings;
        }

        [HttpPost("CreateBooking")]
        public async Task<ActionResult<BookingAppointment>> CreateNewBooking(BookingAppointment createBooking)
        {
            var bookings = await _bookingsService.CreateBooking(createBooking);
            return bookings;
        }

        [HttpGet("GetBookingsbyUserId")]
        public async Task<ActionResult<List<BookingAppointment>>> GetBookingsbyUser([FromBody] BookingAppointment bookingbyUserid)
        {
            var bookings = await _bookingsService.GetBookingsByUserId(bookingbyUserid);
            return bookings;
        }

        [HttpGet("GetBookingsbyDoctorId")]
        public async Task<ActionResult<List<BookingAppointment>>> GetBookingsbyDoctorId([FromBody] BookingAppointment bookingbyDoctorid)
        {
            var bookings = await _bookingsService.GetBookingsByDoctorId(bookingbyDoctorid);
            return bookings;
        }

        [HttpGet("GetBookingsbyId")]
        public async Task<ActionResult<BookingAppointment>> GetBookingsbyID([FromBody] BookingAppointment bookingbyid)
        {
            var bookings = await _bookingsService.GetBookingsById(bookingbyid);
            return bookings;
        }

        [HttpPut("UpdateBooking")]
        public async Task<ActionResult<BookingAppointment>> UpdateBooking([FromQuery] int id, BookingAppointment updateBooking)
        {
            var bookings = await _bookingsService.UpdateBooking(id, updateBooking);
            return bookings;
        }

        [HttpDelete("DeleteBooking")]
        public ActionResult UpdateBooking([FromBody]int id)
        {
            _bookingsService.DeleteBooking(id);
            return Ok();
        }
        [HttpGet("GetAppointmentTypes")]
        public async Task<ActionResult<List<AppointmentType>>> GetAllAppointmentTypes(){

           var AppointemntTypes =  _bookingsService.GetAllAppointmentType();
           return await AppointemntTypes;
        }

    }
}