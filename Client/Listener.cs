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
        private MessageFactory messageFactory;
        private TCPConnection _TCPConnection = new TCPConnection();
        RSA _rsa;
        private string _savedData = null;
        private bool _dataChanged = false;
        public string SavedData
        {
            get { return _savedData; }
            set { _savedData = value; }
        }
        public bool DataChanged
        {
            get { return _dataChanged; }
            set { _dataChanged = value; }
        }

        
        public Listener(IPAddress ip, Int32 port, ref RSA rsa)
        {
            _ip = ip;
            _port = port;
            _rsa = rsa;
            messageFactory = new MessageFactory(_rsa);
            ListiningCondition = true;
        }
        
        public void StartListening()
        {
            _tcpListener = new TcpListener(_ip, _port);
            _tcpListener.Start();

            while (ListiningCondition)
            {
                Byte[] bytes = new Byte[64000];
                String data = null;
                
                _client = _tcpListener.AcceptTcpClient();
               
                data = null;

                NetworkStream stream = _client.GetStream();

                int i;

                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    byte[] byts = Encoding.UTF8.GetBytes(data);
                    Msg msgRecieved = messageFactory.handleRecvMsg(Encoding.UTF8.GetString(byts));

                    if(msgRecieved.getMessageCode() == 150)
                    {
                        Msg150 msg150 = (Msg150)msgRecieved;
                        Msg msgToSend = messageFactory.PeersMessages(msg150.getData(), msg150.getNextRoute());

                        if (_TCPConnection.establishConnection(msg150.getNextAdrressToSendTo().Item1, msg150.getNextAdrressToSendTo().Item2))
                        {
                            if (msgToSend.getMessageCode() == 150)
                            {
                                Msg150 msgToForward = (Msg150)msgToSend;
                                _TCPConnection.SendData(msgToForward.BuildMessageInString());
                                _TCPConnection.disconnect();
                            }
                            else
                            {
                                Msg151 msgToForward = (Msg151)msgToSend;
                                _TCPConnection.SendData(msgToForward.BuildMessageInString());
                                _TCPConnection.disconnect();

                            }
                            //MessageBox.Show("netov tam besalam :)");
                        }
                        else
                        {
                            MessageBox.Show("NO CONNECTION WITH OTHER CLIENT!!!");
                        }
                    }
                    else if(msgRecieved.getMessageCode() == 151)
                    {
                        Msg151 msg151 = (Msg151)msgRecieved;
                        //MessageBox.Show(msg151.getData());
                        SavedData = msg151.getData();
                        DataChanged = true;
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

        internal MessageFactory MessageFactory
        {
            get => default(MessageFactory);
            set
            {
            }
        }
    }
}
