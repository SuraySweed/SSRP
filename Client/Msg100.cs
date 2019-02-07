using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorChatClient
{
    class Msg100 : Msg
    {
        string _nickName;
        
        public Msg100(string nickName)
        {
            _data = null;
            _nickName = nickName;
            _messageCode = 100;
        }

        public override string  BuildMessageInString()
        {
            return "100|" + _nickName;
        }
    }
}
