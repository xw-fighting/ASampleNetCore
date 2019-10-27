using ASample.NetCore.MvcWeb.Models.SignalRs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.MvcWeb.Services
{
    public class ChartJsServices
    {
        private List<VoteParam> voteModelList;
        private int i = 0;

        public ChartJsServices()
        {
            voteModelList = new List<VoteParam>();
            CreateTestVote();
        }

        public void CreateTestVote()
        {
            var voteModel = new VoteParam();
            voteModel.Id = 1;
            voteModel.VoteName = "Test Vote";
            voteModel.Creator = "me";
            voteModel.StartDate = DateTime.Now;
            string[] voteSelect = { "Red", "Blue", "Yellow", "Green", "Purple", "Orange" };
            Dictionary<string, int> _voteSelect = new Dictionary<string, int>();
            for (int i = 0; i < voteSelect.Length; i++)
            {
                _voteSelect.Add(voteSelect[i], 0);
            }
            voteModel.VoteSelect = _voteSelect;
            voteModelList.Add(voteModel);

        }

        // 取得投票Model
        public VoteParam GetVoteModel(string voteName)
        {
            for (int i = 0; i < voteModelList.Count; i++)
            {
                if (voteModelList[i].VoteName == voteName)
                {
                    return voteModelList[i];
                }
            }

            return new VoteParam();
        }

        // 建立新的投票
        public void CreateVote(VoteParam voteModel)
        {
            i++;
            voteModel.Id = i;
            voteModelList.Add(voteModel);
        }

        // 新增投票選項
        public void AddVoteSelect(string voteName, string selectName)
        {
            for (int i = 0; i < voteModelList.Count; i++)
            {
                if (voteModelList[i].VoteName == voteName)
                {
                    voteModelList[i].VoteSelect.Add(selectName, 0);
                }
            }
        }

        // 投票
        public void AddVoteSelectCount(string voteName, string selectName)
        {
            foreach (var voteModel in voteModelList)
            {
                if (voteModel.VoteName == voteName)
                {
                    foreach (var voteSelect in voteModel.VoteSelect)
                    {
                        if (voteSelect.Key == selectName)
                        {
                            voteModel.VoteSelect[selectName] = voteSelect.Value + 1;
                            break;
                        }
                    }
                }
            }
        }

        // 反對票
        public void SubVoteSelectCount(string voteName, string selectName)
        {
            foreach (var voteModel in voteModelList)
            {
                if (voteModel.VoteName == voteName)
                {
                    foreach (var voteSelect in voteModel.VoteSelect)
                    {
                        if (voteSelect.Key == selectName)
                        {
                            voteModel.VoteSelect[selectName] = voteSelect.Value - 1;
                            break;

                        }
                    }
                }
            }
        }


    }
}
