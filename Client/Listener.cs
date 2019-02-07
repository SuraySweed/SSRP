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
    public class Listener
    {
        public TcpListener _tcpListener;
        private TcpClient _client;
        private Int32 _port;
        private IPAddress _ip;
        private bool ListiningCondition { get; set; }
        private MessageFactory messageFactory = new MessageFactory();
        private TCPConnection _TCPConnection = new TCPConnection();
        private string _savedData = null;
        public string SavedData
        {
            get { return _savedData; }
            set { _savedData = value; }
        }

        public Listener(IPAddress ip, Int32 port)
        {
            _ip = ip;
            _port = port;
            ListiningCondition = true;
        }
        
        public void StartListening()
        {
            _tcpListener = new TcpListener(_ip, _port);
            _tcpListener.Start();

            while (ListiningCondition)
            {
                Byte[] bytes = new Byte[256];
                String data = null;
                
                _client = _tcpListener.AcceptTcpClient();
               
                data = null;

                NetworkStream stream = _client.GetStream();

                int i;

                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Msg msgRecieved = messageFactory.handleRecvMsg(data);

                    if(msgRecieved.getMessageCode() == 150)
                    {
                        Msg150 msg150 = (Msg150)msgRecieved;
                        if (_TCPConnection.establishConnection(msg150.getNextAdrressToSendTo().Item1, msg150.getNextAdrressToSendTo().Item2))
                        {
                            Msg150 msgToForward = new Msg150(msg150.getData(), msg150.getNextRoute());
                            _TCPConnection.SendData(msgToForward.BuildMessageInString());
                            _TCPConnection.disconnect();
                        }
                        else
                        {
                            MessageBox.Show("NO CONNECTION WITH OTHER CLIENT!!!");
                        }
                    }
                    else if(msgRecieved.getMessageCode() == 151)
                    {
                        Msg151 msg151 = (Msg151)msgRecieved;
                        MessageBox.Show(msg151.getData());
                        SavedData = msg151.getData();
                    }

                    if (!ListiningCondition)
                        break;
                }

                _client = null;
            }
        }

        public void StopListening()
        {
            _tcpListener.Stop();
        }
        
    }
}
