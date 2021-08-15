using System;
using System.ComponentModel.DataAnnotations.Schema;
using APSystem.Data.Entities;

namespace APSystem.Data.Model
{
    public class AppointmentsModelItem
    {
        public int AppointmentID { get; set; }
        public int? DoctorID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email{get;set;}
        public DateTime? AppointmentDate{ get; set; }
        public int[]? AppointmentTimeSlots { get; set; }
        public int AppointmentSlotID { get; set; }
        public string AppointmentStartTime { get; set; }
        public string AppointmentEndTime { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }


    }
}