using System;

namespace APSystem.Models.Auth
{
    public class RoleResponse : BaseResponse
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }

    }
}
