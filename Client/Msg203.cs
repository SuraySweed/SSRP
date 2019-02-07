using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorChatClient
{
    class Msg203 : Msg
    {
        List<Tuple<string, Int32>> _route;
         
        public Msg203(List<Tuple<string, Int32>> route)
        {
            _data = null;
            _route = route;
            _messageCode = 203;
        }

        public List<Tuple<string, Int32>> GetRoute()
        {
            return _route;
        }
    }
}
