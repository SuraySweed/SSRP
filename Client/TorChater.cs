using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TorChatClient
{
    public partial class TorChater : Form
    {
        public Int32 ServerPORT = 8820;
        public string ServerIPHOST = "127.0.0.1";
        public ClientServerSocket ServerConnection = new ClientServerSocket();
        public TcpListener meListening = null;

        public Protocol protocol = new Protocol();

        public TorChater()
        {
            InitializeComponent();
        }

        private void TorChater_Load(object sender, EventArgs e)
        {


        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            //ServerConnection.disconnect();
            if(meListening != null)
                meListening.Stop();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (ServerConnection.connection(ServerIPHOST, ServerPORT))
            {
                if (nameBox.Text != null && nameBox.Text != "")
                {
                    ServerConnection.SendToServer(protocol.connectToServerMsg(nameBox.Text));

                    List<string> recvMsg = protocol.handleRecvMsg(ServerConnection.ReceiveFromServer());

                    Int32 port = Int32.Parse(recvMsg[1]); ;
                    IPAddress ip = IPAddress.Parse(ServerIPHOST);

                    meListening = new TcpListener(ip, port);
                    MessageBox.Show(recvMsg[0]);
                    meListening.Start();

                    TorChater torChater = this as TorChater;
                    ChatForm chatForm = new ChatForm(ref torChater, ref ServerConnection, nameBox.Text);

                    this.Hide();
                    chatForm.Show();
                }
                else
                {
                    MessageBox.Show("YOU DIDN'T TYPE YOUR NAME!!!");
                }

                ServerConnection.disconnect();
            }
            else
            {
                MessageBox.Show("NO CONNECTION!!!");
            }
        }

        private void nameBox_TextChanged(object sender, EventArgs e)
        {
            if (nameBox.Text != "")
            {
                ConnectButton.Enabled = true;
            }
            else
            {
                ConnectButton.Enabled = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Byte[] bytes = new Byte[256];
            //String data = null;

            //Console.Write("Waiting for a connection... ");

            //TcpClient client = meListening.AcceptTcpClient();
            //Console.WriteLine("Connected!");

            //data = null;

            //NetworkStream stream = client.GetStream();

            //int i;

            //while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            //{
            //    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
            //    //Console.WriteLine("Received: {0}", data);


            //    data = data.ToUpper();

            //    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

            //    //// Send back a response.
            //    //stream.Write(msg, 0, msg.Length);
            //    //Console.WriteLine("Sent: {0}", data);
            //}

        }

        public class ClientServerSocket
        {
            private TcpClient client;
            private NetworkStream clientStream;

            public TcpClient getClient()
            {
                return client;
            }

            public void disconnect()
            {
                client.Close();
            }

            public bool connection(string ip, int port)
            {
                try
                {
                    client = new TcpClient();
                    IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
                    client.Connect(serverEndPoint);
                    clientStream = client.GetStream();

                }
                catch (Exception e)
                {

                }

                if (client.Connected)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public void SendToServer(string msgToServer)
            {
                byte[] buffer = new ASCIIEncoding().GetBytes(msgToServer);
                clientStream.Write(buffer, 0, buffer.Length);
                clientStream.Flush();
            }

            public string ReceiveFromServer()
            {
                try
                {
                    byte[] bufferln = new byte[4800];
                    int bytesRead = clientStream.Read(bufferln, 0, 4800);
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
}
