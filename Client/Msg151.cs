using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorChatClient
{
    class Msg151 : Msg
    {
      
        public Msg151(string data)
        {
            _data = data;
            _messageCode = 151;
        }

        public override string BuildMessageInString()
        {
            return "151||||" + _data;
        }
    }
}
