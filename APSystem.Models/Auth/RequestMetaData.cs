using System.Collections.Generic;

namespace APSystem.Models.Auth
{
    public class RequestMetaData
    {
        public RequestMetaData()
        {
            this.AuthData = new AuthResponse();
            this.Attributes = new Dictionary<string, string>();
        }
        public AuthResponse AuthData { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
    }
}