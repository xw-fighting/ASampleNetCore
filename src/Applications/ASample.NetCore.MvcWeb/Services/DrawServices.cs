using ASample.NetCore.MvcWeb.Models.SignalRs;
using System.Collections.Generic;

namespace ASample.NetCore.MvcWeb.Services
{
    public class DrawServices
    {
        private List<DrawParam> _list;
        public DrawServices()
        {
            _list = new List<DrawParam>();
        }
        public void AddList(DrawParam drawModel)
        {
            _list.Add(drawModel);
        }
        public List<DrawParam> GetList()
        {
            return _list;
        }
    }
}
