using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APSystem.Data.Entities
{
    [Table("tblApHistory")]
    public class AppointmentHistoryDbEntity:BaseEntity
    {
        [Key]
    public int ApHistoryID { get; set; }
    public int? UserID { get; set; }
    public int? BookingID { get; set; }
    }
}