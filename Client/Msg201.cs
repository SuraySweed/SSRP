using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TorChatClient
{
    class Msg201 : Msg
    {
        Int32 _port;
        IPAddress _ip;

        public Msg201(Int32 port, IPAddress iPAddress, string data)
        {
            _port = port;
            _ip = iPAddress;
            _data = data;
        }
    }
}
