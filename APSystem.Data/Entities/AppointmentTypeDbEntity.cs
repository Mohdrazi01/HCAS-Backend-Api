using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APSystem.Data.Entities
{
    [Table("tblAppointmentType")]
    public class AppointmentTypeDbEntity:BaseEntity
    {
      [Key]
    public int AppointmentTypeID { get; set; }
    public string AppointmentTypes { get; set; }
    public int? ApHistoryID { get; set; } 
    }
}