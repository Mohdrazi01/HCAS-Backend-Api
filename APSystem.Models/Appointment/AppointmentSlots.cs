using System;

namespace APSystem.Models.Appointment
{
    public class AppointmentSlots
    {
        public int AppointmentSlotID { get; set; }
        public TimeSpan? AppointmentStartTime { get; set; } = TimeSpan.FromHours(00)+ TimeSpan.FromMinutes(00)+ TimeSpan.FromSeconds(00);
        public TimeSpan? AppointmentEndTime { get; set; }  = TimeSpan.FromHours(00)+ TimeSpan.FromMinutes(00)+ TimeSpan.FromSeconds(00);

        //= TimeSpan.FromHours(00)+ TimeSpan.FromMinutes(00)+ TimeSpan.FromSeconds(00)
    }
}