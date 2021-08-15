using System.ComponentModel;
using System;
using System.IO;
using System.Threading.Tasks;
using APSystem.Core.Controllers.Chat.ReqDto;
using APSystem.Core.Hubs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace APSystem.Core.Controllers.Chat
{
    [Route("api/v1/Chat")]
    [ApiController]
    public class ChatController: BaseController<ChatController>
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IWebHostEnvironment _environment;
       public ChatController(ILogger<ChatController> logger,IHubContext<ChatHub> hubContext, IWebHostEnvironment environment): base(logger)
       {
           _hubContext = hubContext;
           _environment = environment;
       }

       [HttpPost("send")]
       public ActionResult SendRequest(MessageDto msg)
       {

           _hubContext.Clients.All.SendAsync("ReceiveMessage",msg.user,msg.messageText);
          return Ok();
       }
    }
}
