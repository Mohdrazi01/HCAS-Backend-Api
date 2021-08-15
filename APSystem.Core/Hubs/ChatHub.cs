using System.Net.Http;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using APSystem.Core.Controllers.Chat.ReqDto;

namespace APSystem.Core.Hubs
{
    public class ChatHub:Hub
    {
      public ChatHub(){

      }
      public async  Task SendMessage(MessageDto msg)
      {
         await Clients.All.SendAsync("ReceiveMessage", msg.user,msg.messageText);
        //  await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage",msg.user,msg.messageText,msg.img);
        //  await Clients.Caller.SendAsync("ReceiveMessage",msg.user, msg.messageText,msg.img);
      }

    }
}
