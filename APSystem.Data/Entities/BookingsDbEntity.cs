using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APSystem.Data.Entities
{
    [Table("tblApBookings")]
    public class BookingsDbEntity:BaseEntity
    {
        [Key]
    public int BookingID { get; set; }
    public int? PatientID { get; set; }
    public int? DoctorID {get;set;}
    public int? AppointmentID { get; set; }
    public int? AppointmentSlotID {get; set;}
    public int? AppointmentTypeID { get; set; }
    public string PhoneNumber { get; set; }
    public string ProblemDiscription { get; set; }
    public int? StatusID {get;set;}
    
    }

}