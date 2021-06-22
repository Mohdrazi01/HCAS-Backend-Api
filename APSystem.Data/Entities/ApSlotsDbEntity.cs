using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APSystem.Data.Entities
{
    [Table("tblAppointmentSlots")]
    public class ApSlotsDbEntity:BaseEntity
    {
        [Key]
         public int AppointmentSlotID { get; set; }  
        public TimeSpan? AppointmentTimeSlots { get; set; }  
    }
}