using ASample.NetCore.MvcWeb.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.MvcWeb.Hubs
{
    public class FileHub:Hub
    {
        private FileServices _service;
        public FileHub(FileServices service)
        {
            _service = service;
        }

        public async Task GetFile(string fileName)
        {
            var file = _service.GetFile(fileName);
            await Clients.Caller.SendAsync("ReceiveFile", file);
        }


        public async Task EditText(string fileName, string cellName, string text)
        {
            var editText = _service.EditFileCell(fileName, cellName, text);
            await Clients.All.SendAsync("ReceiveEditText", cellName, editText.Text);
        }

        // 連線
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        // 斷線
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
