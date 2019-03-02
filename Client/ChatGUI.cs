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
        List<string> _keys;
        public string goesRightToChat;

        ConnectToTorChat _ConnectToTorChatForm;
        TCPConnection _connection = new TCPConnection();
        Thread thread;
        MessageFactory messageFactory;


        public ChatGUI(ref ConnectToTorChat CTTC, string mainName)
        {
            _ConnectToTorChatForm = CTTC;
            _mainName = mainName;
            messageFactory = new MessageFactory(_ConnectToTorChatForm._rsa);
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
                    // TODO: need to collect the keys from the message
                    /**********************************************/
                    /**********************************************/
                    /**********************************************/
                    /**********************************************/

                    Msg203 msg203 = (Msg203)rcvdMsg;
                    _currentRoute = msg203.GetRoute();
                    _keys = msg203.GetKeysInRoute();

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
                //string dataToSend = messageFactory.PeersMessages((_mainName + ": " + messageToSendBox.Text), _currentRoute).BuildMessageInString();
                //_rsaEncryption.Encrypt(Encoding.UTF8.GetBytes(dataToSend));

                string dataToSend = _ConnectToTorChatForm._rsa.EncryptMessageSeveralTimes(messageFactory.PeersMessages((_mainName + ": " + messageToSendBox.Text), _currentRoute).BuildMessageInString(), _keys);

                _connection.SendData(dataToSend);
                _connection.disconnect();

                ChatText.Text += "me: " + messageToSendBox.Text;
                ChatText.Text += "\n";
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

        private void ChatText_TextChanged(object sender, EventArgs e)
        {

        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("he says" + _ConnectToTorChatForm.meListening.SavedData);
            ChatText.Text += _ConnectToTorChatForm.meListening.SavedData;
            ChatText.Text += "\n";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_ConnectToTorChatForm.meListening.DataChanged)
            {
                ChatText.Text += _ConnectToTorChatForm.meListening.SavedData;
                ChatText.Text += "\n";

                _ConnectToTorChatForm.meListening.DataChanged = false;
            }
        }
    }
}
