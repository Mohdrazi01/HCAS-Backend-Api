using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APSystem.Data.Entities
{
    [Table("tblApStatus")]
    public class AppointmentStatusDbEntity:BaseEntity
    {
        [Key]
        public int ApStatusID { get; set; }  
         public string ApStatus { get; set; }  
    }
}