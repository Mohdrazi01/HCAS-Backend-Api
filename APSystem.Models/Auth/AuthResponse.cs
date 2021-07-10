namespace APSystem.Models.Auth
{
    public class AuthResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public int RoleId { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public string Access_Token { get; set; }

        public bool IsEmailConfirmed {get;set;}

    }
}