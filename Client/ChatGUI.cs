using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TorChatClient
{
    public partial class ChatGUI : Form
    {
        private static Mutex mut = new Mutex();
        private string otherClientName;
        private string recepientIP;
        private int recepientPORT;
        private string _mainName;
        private List<Tuple<string, Int32>> _currentRoute;
        public string goesRightToChat;

        ConnectToTorChat _ConnectToTorChatForm = new ConnectToTorChat();
        TCPConnection _connection = new TCPConnection();
        Thread thread;
        MessageFactory messageFactory = new MessageFactory();


        public ChatGUI(ref ConnectToTorChat CTTC, string mainName)
        {
            _ConnectToTorChatForm = CTTC;
            _mainName = mainName;
            InitializeComponent();
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
            thread = new Thread(new ThreadStart(_ConnectToTorChatForm.meListening.StartListening));
            thread.Start();
        }

        private void exitBut_Click(object sender, EventArgs e)
        {
            _ConnectToTorChatForm.Close();
            _ConnectToTorChatForm.meListening.StopListening();
            thread.Join();
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (NameOfOther.Text != "")
            {
                getNameButton.Enabled = true;
            }
            else
            {
                getNameButton.Enabled = false;
            }
        }

        private void getNameButton_Click(object sender, EventArgs e)
        {
            if (_connection.establishConnection(_ConnectToTorChatForm._PortAndIPOfServer.GetServerIP(), _ConnectToTorChatForm._PortAndIPOfServer.GetServerPort()))
            {

                _connection.SendData(messageFactory.sendRecepientNameMsg(_mainName , NameOfOther.Text).BuildMessageInString());

                Msg rcvdMsg =  messageFactory.handleRecvMsg(_connection.ReceiveData());
                

                if (rcvdMsg == null)
                {
                    MessageBox.Show("no such name");
                }
                else
                {
                    Msg203 msg203 = (Msg203)rcvdMsg;
                    _currentRoute = msg203.GetRoute();

                    recepientIP = _currentRoute[_currentRoute.Count - 1].Item1;
                    recepientPORT = _currentRoute[_currentRoute.Count - 1].Item2;
                    _currentRoute.Remove(new Tuple<string, int>(recepientIP, recepientPORT));

                    getNameButton.Hide();
                    NameOfOther.Hide();
                    otherName.Text = NameOfOther.Text;
                    otherClientName = NameOfOther.Text;
                    otherName.Show();
                    disconnectButton.Show();
                }

                _connection.disconnect();
            }
            else
            {
                MessageBox.Show("NO CONNECTION!!!");
            }


        }

        //private void startListening()
        //{
        //    while (threadCondition)
        //    {
        //        Byte[] bytes = new Byte[256];
        //        String data = null;

        //        Console.Write("Waiting for a connection... ");

        //        TcpClient client = _ConnectToTorChatForm.meListening.AcceptTcpClient();
        //        Console.WriteLine("Connected!");

        //        data = null;

        //        NetworkStream stream = client.GetStream();

        //        int i;

        //        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
        //        {
        //            data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

        //            string stuff = handleMessagesFromOtherClients(data);

        //            //ChatText.Text += stuff;

        //            data = data.ToUpper();

        //            byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

        //            if (!threadCondition)
        //                break;
        //        }
        //    }
        //}

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (messageToSendBox.Text != "" && disconnectButton.Visible == true)
            {
                sendMessageButton.Enabled = true;
            }
            else
            {
                sendMessageButton.Enabled = false;
            }
        }

        private void sendMessageButton_Click(object sender, EventArgs e)
        {
            if (_connection.establishConnection(recepientIP, recepientPORT))
            {
                _connection.SendData(messageFactory.PeersMessages(messageToSendBox.Text, _currentRoute).BuildMessageInString());
                _connection.disconnect();
            }
            else
            {
                MessageBox.Show("NO CONNECTION WITH OTHER CLIENT!!!");
            }
        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
            recepientIP = null;
            recepientPORT = 0;
            getNameButton.Show();
            NameOfOther.Text = "";
            NameOfOther.Show();
            otherName.Hide();
            disconnectButton.Hide();

        }

        //private dynamic handleMessagesFromOtherClients(string message)
        //{
        //    string[] splitedMSG = message.Split('|');
        //    if(splitedMSG[0] == "150") // forward
        //    {
        //        recepientPORT = Int32.Parse(splitedMSG[splitedMSG.Length - 2].Split(',')[1]);
        //        recepientIP = splitedMSG[splitedMSG.Length - 2].Split(',')[0];

        //        string msgToSend = null;

        //        if (splitedMSG.Length == 4)
        //            msgToSend = "151|";
        //        else
        //            msgToSend = "150|";

        //        goesRightToChat = null;

        //        for (int i = 1; i < splitedMSG.Length - 2; i++) 
        //        {
        //            msgToSend += splitedMSG[i] + "|";
        //        }

        //        if (_connection.connection(recepientIP, recepientPORT))
        //        {
        //            _connection.SendToServer(msgToSend);
        //            _connection.disconnect();
        //        }
        //        else
        //        {
        //            MessageBox.Show("NO CONNECTION WITH OTHER CLIENT!!!");
        //        }
        //        return null;
        //    }
        //    else if(splitedMSG[0] == "151")// got a message
        //    {
        //        goesRightToChat += splitedMSG[1];
        //        goesRightToChat += "\n";
                

        //        return goesRightToChat;
        //    }
        //    return null;
        //}

        private void ChatText_TextChanged(object sender, EventArgs e)
        {

        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("he says" + _ConnectToTorChatForm.meListening.SavedData);
            ChatText.Text += _ConnectToTorChatForm.meListening.SavedData;
            ChatText.Text += "\n";
        }
    }
}
