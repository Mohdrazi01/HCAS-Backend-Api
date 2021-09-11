using System;

namespace APSystem.Models.Appointment
{
    public class AppointmentwithSlotsjoin : BaseResponse
    {
        public int AppointmentID { get; set; }
        public int? DoctorID { get; set; }
        public int UserID{get; set;}
        public string Name{get;set;}
        public string Email {get;set;}
        public DateTime? AppointmentDate { get; set; }
        public int? AppointmentTimeSlots { get; set; }
        public int AppointmentSlotID { get; set; }
        public string AppointmentStartTime { get; set; }
        public string AppointmentEndTime { get; set; }


    }
}