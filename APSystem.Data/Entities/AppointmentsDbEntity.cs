using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APSystem.Data.Entities
{
    [Table("tblAppointments")]
    public class AppointmentsDbEntity:BaseEntity
    {

    [Key]
    public int AppointmentID { get; set; }  
    public int? DoctorID { get; set; }  
    public DateTime? AppointmentDate { get; set; }  
    public int? AppointmentTimeSlots { get; set; }
    }
}