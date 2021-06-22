using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APSystem.Data.Entities
{
  [Table("tblApFeedback")]
    public class ApFeedbackDbEntity:BaseEntity
    {
      [Key]
      public int ApFeedbackID { get; set; }
      public string Review { get; set; }
      public int? ApHistoryID { get; set; }
    }
}