using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace APSystem.Core.Controllers.Chat.ReqDto
{
    public class MessageDto
    {
        public string user { get; set; }
        public string messageText { get; set; }
        
        // public ICollection<IFormFile>? File { get; set; }
        // public byte[]? ImageBinary { get; set; }
        // public string ImageHeaders { get; set; }

    }
}
