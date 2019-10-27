using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASample.NetCore.MvcWeb.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ASample.NetCore.MvcWeb.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class ChatController : Controller
    {
        private readonly IHubContext<ChatHub> _hubContext;
        public ChatController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }
        [HttpGet("SendMessage")]
        public async Task SendMessage([FromQuery]string user, string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        //[HttpGet("AddToGroup")]
        //public async Task AddToGroup([FromQuery]string groupName, string username)
        //{
        //    await _hubContext.Clients.Group.SendAsync("ReceiveMessage", user, message);
        //}
    }
}