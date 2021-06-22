using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APSystem.Data.Entities
{
    [Table("tblDoctors")]
    public class DoctorDbEntity:BaseEntity
    {
        [Key]
    public int UserID { get; set; }  
    public string UserName { get; set; }  
    public int? RoleID { get; set; }  
    public string Email { get; set; }  
    public string Password { get; set; }  
    public int? GMCNumber { get; set; }  
    public string Speciality { get; set; }  
    public string Experience { get; set; }  
    public int? PhoneNumber { get; set; }  
    public DateTime? DateOfBirth { get; set; }  
    public string Gender { get; set; }  
    public string Address { get; set; }
    }
}