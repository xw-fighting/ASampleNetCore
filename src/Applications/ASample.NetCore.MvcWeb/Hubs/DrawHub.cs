using ASample.NetCore.MvcWeb.Models.SignalRs;
using ASample.NetCore.MvcWeb.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.MvcWeb.Hubs
{
    public class DrawHub:Hub
    {
        public DrawServices _drawService;
        public DrawHub(DrawServices drawService)
        {
            _drawService = drawService;
        }
        public async Task SendDraw(DrawParam drawModel)
        {
            _drawService.AddList(drawModel);
            await Clients.All.SendAsync("ReceiveDraw", drawModel);
        }
        public async Task GetDraw()
        {
            await Clients.Caller.SendAsync("ReciveAllDraw", _drawService.GetList());
        }
    }
}
