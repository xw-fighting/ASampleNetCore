using ASample.NetCore.Dispatchers;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.SignalRWeb.Hubs
{
    public class ChatHub:Hub
    {
        private readonly IDispatcher _disppatcher;

        public ChatHub(IDispatcher disppatcher)
        {
            _disppatcher = disppatcher;
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendUserMessage(string user, string message)
        {
            await Clients.Caller.SendAsync("ReceiveUserMessage", user, message);
        }

        public async Task SendGroupMessage(string groupName,string userName, string message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveGroupMessage", userName, message);
        }





    }
}
