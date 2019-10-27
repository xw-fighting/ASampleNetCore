using ASample.NetCore.MvcWeb.Models.SignalRs;
using ASample.NetCore.MvcWeb.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.MvcWeb.Hubs
{
    public class ChatHub : Hub
    {
        private UserServices conUserList;
        public ChatHub(UserServices conUserService)
        {
            conUserList = conUserService;
        }
        public async Task SendMessage(string user, string message)
        {
            var responseJson = new ReponseJson()
            {
                User = user,
                Message = message
            };
            await Clients.All.SendAsync("ReceiveMessage", responseJson);
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

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
            await Clients.Group("TestGroup").SendAsync("GetConList", conUserList.RemoveList(Context.ConnectionId));
        }

        public async void AddConUserList(string user, string groupName)
        {
            var _user = new SignalRUser();
            _user.UserName = user;
            _user.ConnectID = Context.ConnectionId;
            _user.OnlineTime = DateTime.Now;
            _user.GroupName = groupName;

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("GetConList", conUserList.AddList(_user));
        }

    }
}
