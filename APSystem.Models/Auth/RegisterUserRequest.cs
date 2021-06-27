using System;

namespace APSystem.Models.Auth
{
    public class RegisterUserRequest
    {
    public string Name { get; set; }  
    public string Email { get; set; }  
    public string Password { get; set; }  
    public string PhoneNumber { get; set; }  
    public DateTime DateOfBirth { get; set; }  
    public string Gender { get; set; }  
    public string Address { get; set; }  
    }
}