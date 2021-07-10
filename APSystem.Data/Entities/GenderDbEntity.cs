using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APSystem.Data.Entities
{
    [Table("tblGender")]
    public class GenderDbEntity:BaseEntity
    {
        [Key]
       public int GenderId { get; set; }  
       public string GenderName { get; set; }  
        
        
    }
}