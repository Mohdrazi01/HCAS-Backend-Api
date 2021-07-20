using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace APSystem.Models.Appointment
{
    public class Appointments 
    {
        public int AppointmentID { get; set; }
        public int? DoctorID { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public int? AppointmentTimeSlots { get; set; }
    }
}