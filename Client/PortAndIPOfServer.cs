using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorChatClient
{
    public class PortAndIPOfServer
    {
        private Int32 ServerPORT = 8820;
        private string ServerIPHOST = "10.0.0.12";

        public Int32 GetServerPort()
        {
            return ServerPORT;
        }

        public string GetServerIP()
        {
            return ServerIPHOST;
        }
    }
}
