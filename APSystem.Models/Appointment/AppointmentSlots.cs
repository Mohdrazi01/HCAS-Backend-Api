using System;

namespace APSystem.Models.Appointment
{
    public class AppointmentSlots : BaseResponse
    {
        public int AppointmentSlotID { get; set; }
        public string AppointmentStartTime { get; set; }
        public string AppointmentEndTime { get; set; } 
    }
}