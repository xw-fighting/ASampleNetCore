using ASample.NetCore.MvcWeb.Models.SignalRs;
using ASample.NetCore.MvcWeb.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.MvcWeb.Hubs
{
    public class ChartJsHub:Hub
    {

        private ChartJsServices _voteService;
        public ChartJsHub(ChartJsServices voteService)
        {
            _voteService = voteService;
        }

        public async Task GetVote(string voteName)
        {
            await Clients.Caller.SendAsync("ReceiveVote", _voteService.GetVoteModel(voteName));
        }

        public async Task AddVoteSelectCount(string voteName, string voteSelect)
        {
            _voteService.AddVoteSelectCount(voteName, voteSelect);

            var temp = _voteService.GetVoteModel(voteName);
            int count = 0;
            foreach (var _voteSelect in temp.VoteSelect)
            {
                if (_voteSelect.Key == voteSelect)
                {
                    count = _voteSelect.Value;
                }
            }

            await Clients.All.SendAsync("ReceiveVoteSelectCount", count, voteSelect);
        }

        public async Task SubVoteSelectCount(string voteName, string voteSelect)
        {
            _voteService.SubVoteSelectCount(voteName, voteSelect);

            var temp = _voteService.GetVoteModel(voteName);
            int count = 0;
            foreach (var _voteSelect in temp.VoteSelect)
            {
                if (_voteSelect.Key == voteSelect)
                {
                    count = _voteSelect.Value;
                }
            }

            await Clients.All.SendAsync("ReceiveVoteSelectCount", count, voteSelect);
        }
    }
}
