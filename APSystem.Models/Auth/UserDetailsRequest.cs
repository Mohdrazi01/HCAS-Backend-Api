using System;

namespace APSystem.Models.Auth
{
    public class UserDetailsRequest
    {
         public int? UserID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public int RoleID { get; set; }
    }
}
