using System.Globalization;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APSystem.Data.Entities
{
    [Table("tblPatients")]
    public class PatientDbEntity : BaseEntity
    {
    [Key]
    public int UserID { get; set; }  
    public string Name{ get; set; }
    public string UserName { get; set; }  
    public int RoleID { get; set; }  
    public string Email { get; set; }  
    public string Password { get; set; }  
    public String PhoneNumber { get; set; }  
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string Address { get; set; }
    [NotMapped]
    public bool IsUserNameExist { get; set; }
    [NotMapped]
    public bool IsUserCreated { get; set; }
    
    public Guid? EmailActivationCode { get; set; }
    
    }
}