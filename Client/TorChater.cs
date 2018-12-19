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
           

        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string getPaddedNumber(int number, int numberOfDigits)
        {
            string toReturn = number.ToString();
            toReturn = toReturn.PadLeft(numberOfDigits, '0');
            return toReturn;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if(ServerConnection.connection())
            {
                if (nameBox.Text != null && nameBox.Text != "")
                {
                    ServerConnection.SendToServer("100|" + nameBox.Text);

                    string recvMsg = ServerConnection.ReceiveFromServer();
                    fstMsg.Hide();
                    nameBox.Hide();
                    ConnectButton.Hide();
                    MessageBox.Show(recvMsg);
                    SendButton.Show();
                    scndTxt.Show();
                    MsgBox.Show();
                    thrdTxt.Show();
                    recepientName.Show();
                }
                else
                {
                    MessageBox.Show("YOU DIDN'T TYPE YOUR NAME!!!");
                }
            }
            else
            {
                MessageBox.Show("NO CONNECTION!!!");
            }
        }

        private void nameBox_TextChanged(object sender, EventArgs e)
        {
            if(nameBox.Text != "")
            {
                ConnectButton.Enabled = true;
            }
            else
            {
                ConnectButton.Enabled = false;
            }
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MsgBox.Text == "")
                {
                    notes.Text = "you need to type a message!";
                }
                else if (recepientName.Text == "")
                {
                    notes.Text = "you need to type the destination!";
                }
                else
                {
                    // connect to other fellow 
                    string msgToSend = "102|" + recepientName.Text;
                    ServerConnection.SendToServer(msgToSend);
                    // getting his info (address)--> port, ip
                    string infoOnOtherSide = ServerConnection.ReceiveFromServer();
                    string[] list = infoOnOtherSide.Split('|');

                }
            }
            catch (Exception)
            {

            }
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
