namespace APSystem.Models.Bookings
{
    public class AppointmentType : BaseResponse
    {
        public int AppointmentTypeID { get; set; }
        public string AppointmentTypes { get; set; }
        public int? ApHistoryID { get; set; }

    }
}