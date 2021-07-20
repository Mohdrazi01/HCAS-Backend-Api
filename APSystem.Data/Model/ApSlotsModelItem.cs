using System;

namespace APSystem.Data.Model
{
    public class ApSlotsModelItem
    {
        public int AppointmentSlotID { get; set; }
        public TimeSpan? AppointmentStartTime { get; set; }
        public TimeSpan? AppointmentEndTime { get; set; }
    }
}