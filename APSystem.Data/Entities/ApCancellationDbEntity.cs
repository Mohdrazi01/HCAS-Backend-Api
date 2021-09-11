using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APSystem.Data.Entities
{
    [Table("tblApCancellation")]
    public class ApCancellationDbEntity :BaseEntity
    {
        [Key]
        public int ApCancellationID { get; set; }
        public string ApCancellationReason { get; set; }
    }
}