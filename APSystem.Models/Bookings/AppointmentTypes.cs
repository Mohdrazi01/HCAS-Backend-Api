namespace APSystem.Models.Bookings
{
    public class AppointmentTypes : BaseResponse
    {
        public int AppointmentTypeID { get; set; }
        public string AppointmentType { get; set; }
        public int? ApHistoryID { get; set; }

    }
}