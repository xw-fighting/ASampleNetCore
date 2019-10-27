using ASample.NetCore.MvcWeb.Models.SignalRs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASample.NetCore.MvcWeb.Services
{
    public class UserServices
    {
        private List<SignalRUser> _list;
        public UserServices()
        {
            _list = new List<SignalRUser>();
        }
        public List<SignalRUser> AddList(SignalRUser user)
        {
            _list.Add(user);
            return _list;
        }
        public List<SignalRUser> RemoveList(string connectID)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                if (_list[i].ConnectID == connectID)
                    _list.Remove(_list[i]);
            }
            return _list;
        }
    }
}
