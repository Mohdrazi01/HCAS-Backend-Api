using System;
using System.ComponentModel.DataAnnotations.Schema;
using APSystem.Data.Entities;

namespace APSystem.Data.Model
{
    public class AppointmentsModelItem
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