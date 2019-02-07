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
    public class TCPConnection
    {
        private TcpClient _client;
        private NetworkStream _clientStream;

        public TcpClient getClient()
        {
            return _client;
        }

        public void disconnect()
        {
            _client.Close();
        }

        public bool establishConnection(string ip, int port)
        {
            try
            {
                _client = new TcpClient();
                IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
                _client.Connect(serverEndPoint);
                _clientStream = _client.GetStream();

            }
            catch (Exception e)
            {

            }

            if (_client.Connected)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SendData(string msg)
        {
            byte[] buffer = new ASCIIEncoding().GetBytes(msg);
            _clientStream.Write(buffer, 0, buffer.Length);
            _clientStream.Flush();
        }

        public string ReceiveData()
        {
            try
            {
                byte[] bufferln = new byte[4800];
                int bytesRead = _clientStream.Read(bufferln, 0, 4800);
                string message = new ASCIIEncoding().GetString(bufferln);
                return message;
            }
            catch (Exception e)
            {
                MessageBox.Show("Connection Error, " + e.ToString());
            }
            return "";
        }
    }
}
