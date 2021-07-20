using System;

namespace APSystem.Models.Appointment
{
    public class AppointmentwithSlotsjoin
    {
        public int AppointmentID { get; set; }
        public int? DoctorID { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public int? AppointmentTimeSlots { get; set; }
        public int AppointmentSlotID { get; set; }
        public TimeSpan? AppointmentStartTime { get; set; }
        public TimeSpan? AppointmentEndTime { get; set; }


    }
}