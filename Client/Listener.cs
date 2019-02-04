using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TorChatClient
{
    class Listener
    {
        public TcpListener _tcpListener;
        private TcpClient _client;
        private Int32 _port;
        private IPAddress _ip;

        public Listener(IPAddress ip, Int32 port)
        {
            _ip = ip;
            _port = port;
        }
        
        public void startListening()
        {
            _tcpListener = new TcpListener(_ip, _port);
            _tcpListener.Start();
        }

        public void stopListening()
        {
            _tcpListener.Stop();
        }
        
    }
}
