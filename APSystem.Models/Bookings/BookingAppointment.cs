using System;

namespace APSystem.Models.Bookings
{
    public class BookingAppointment: BaseResponse
    {
        public int BookingID { get; set; }
        public int? PatientID { get; set; }
        public string PatientName { get; set; }
        public string PatientEmail { get; set; }
        public int? DoctorID { get; set; }
        public string DoctorName { get; set; }
        public string DoctorEmail { get; set; }
        public int? AppointmentID { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string AppointmentStartTime { get; set; }
        public string AppointmentEndTime { get; set; }
        public int? AppointmentTypeID { get; set; }
        public string AppointmentType { get; set; }
        public string PhoneNumber { get; set; }
        public string ProblemDiscription { get; set; }
        public int? StatusID { get; set; }
        public string ApStatus { get; set; }

    }
}