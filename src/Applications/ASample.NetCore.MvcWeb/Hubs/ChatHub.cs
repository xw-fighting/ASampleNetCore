using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.MvcWeb.Hubs
{
    public class ChatHub:Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task AddGroup(string groupName, string username)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("RecAddGroupMsg", $"{username} 已加入 群組：{groupName}。");
        }

        public Task SendMessageToGroup(string groupName, string username, string message)
        {
            return Clients.Group(groupName).SendAsync("ReceiveGroupMessage", groupName, username, message);
        }
    }
}
