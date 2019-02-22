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
        List<string> _keys;
         
        public Msg203(List<Tuple<string, Int32>> route, List<string> keys)
        {
            _data = null;
            _route = route;
            _keys = keys;
            _messageCode = 203;
        }

        public List<Tuple<string, Int32>> GetRoute()
        {
            return _route;
        }

        public List<string> GetKeysInRoute()
        {
            return _keys;
        }
    }
}
