using System;

namespace APSystem.Models.Auth
{
    public class RegisterUserRequest
    {
    public string Name { get; set; }
    public int RoleID { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int Gender { get; set; }
    public int? GMCNumber { get; set; }
    public string Speciality { get; set; }
    public string Experience { get; set; }
    public string Address { get; set; }
    }
}