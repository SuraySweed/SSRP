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
        public ClientServerSocket ServerConnection = new ClientServerSocket();

        public TorChater()
        {
            InitializeComponent();
        }

        private void TorChater_Load(object sender, EventArgs e)
        {
            bool check = true;

            while (check)
            {
                check = ServerConnection.connection();
                if (check)
                {
                    break;
                }
                else
                {
                    MessageBox.Show("NO CONNECTION!!");
                    this.Close();
                }
            }
            recievedMessages.Text = ServerConnection.ReceiveFromServer(); // receiving the welcoming message

        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if (typeCommand.Text != "Enter Your Name:")
            {
                if (MessangerBox.Text == null || MessangerBox.Text == "")
                {
                    MessageBox.Show("NO MESSAGE WRITTEN!!!");
                }
                else if (nameBox.Text == null || nameBox.Text == "")
                {
                    MessageBox.Show("NO DESTINATION!!!");
                }
                else
                {
                    string msgToServer = MessangerBox.Text;
                    ServerConnection.SendToServer(msgToServer);

                    string messageFromServer = ServerConnection.ReceiveFromServer();
                    recievedMessages.Text = messageFromServer;
                }
            }
            else
            {

                if (MessangerBox.Text == null || MessangerBox.Text == "")
                {
                    MessageBox.Show("NO MESSAGE WRITTEN!!!");
                }
                else
                {
                    string msgToServer = MessangerBox.Text;
                    nameBox.Text = MessangerBox.Text;
                    ServerConnection.SendToServer(msgToServer);

                    string messageFromServer = ServerConnection.ReceiveFromServer();
                    recievedMessages.Text = messageFromServer;

                    recievedMessages.Visible = true;
                    recievedMessages.Text = "";
                    nameBox.Visible = true;
                    nameBox.Text = "";
                    typeCommand.Text = "Message To Send:";
                    typeCommand.Visible = true;
                    label1.Visible = true;
                    serverResponse.Visible = true;
                }
            }
        }

        public string getPaddedNumber(int number, int numberOfDigits)
        {
            string toReturn = number.ToString();
            toReturn = toReturn.PadLeft(numberOfDigits, '0');
            return toReturn;
        }

        private void typeCommand_Click(object sender, EventArgs e)
        {

        }
    }

    public class ClientServerSocket
    {
        private TcpClient client;
        private NetworkStream clientStream;

        public TcpClient getClient()
        {
            return client;
        }

        public bool connection()
        {
            try
            {
                client = new TcpClient();
                IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8820);
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
