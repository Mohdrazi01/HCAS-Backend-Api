using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace APSystem.Data.Model
{
    public class UserModel
    {
        public int? UserID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public string GenderName { get; set; }
        public int? GMCNumber { get; set; }
        public string Speciality { get; set; }
        public string Experience { get; set; }
        public string Address { get; set; }
        [NotMapped]
        public bool IsUserNameExist { get; set; }
        [NotMapped]
        public bool IsUserCreated { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public Guid? EmailActivationCode { get; set; }
    }
}