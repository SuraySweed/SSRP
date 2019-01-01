﻿using System;
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
    public partial class ChatForm : Form
    {
        private static Mutex mut = new Mutex();
        private string otherClientName;
        private string recepientIP;
        private int recepientPORT;
        bool threadCondition = true;
        private string _mainName;

        TorChater _torChater = new TorChater();
        TorChater.ClientServerSocket _serverConnect = new TorChater.ClientServerSocket();
        Thread thread;


        public ChatForm(ref TorChater torChater, ref TorChater.ClientServerSocket clientServerSocket, string mainName)
        {
            _serverConnect = clientServerSocket;
            _torChater = torChater;
            _mainName = mainName;
            InitializeComponent();
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
            thread = new Thread(new ThreadStart(startListening));
            thread.Start();
        }

        private void exitBut_Click(object sender, EventArgs e)
        {
            threadCondition = false;
            _serverConnect.disconnect();
            _torChater.Close();
            _torChater.meListening.Stop();
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
            if (_serverConnect.connection(_torChater.ServerIPHOST, _torChater.ServerPORT))
            {

                _serverConnect.SendToServer(_torChater.protocol.sendRecepientNameMsg(NameOfOther.Text));

                List<string> infoOnOtherSide = _torChater.protocol.handleRecvMsg(_serverConnect.ReceiveFromServer());

                if (infoOnOtherSide == null)
                {
                    MessageBox.Show("no such name");
                }
                else
                {
                    //MessageBox.Show(infoOnOtherSide[0] + "\n" + infoOnOtherSide[1]);
                    recepientIP = infoOnOtherSide[0];
                    recepientPORT = Int32.Parse(infoOnOtherSide[1]);
                    getNameButton.Hide();
                    NameOfOther.Hide();
                    otherName.Text = NameOfOther.Text;
                    otherClientName = NameOfOther.Text;
                    otherName.Show();
                    disconnectButton.Show();
                   
                }

                _serverConnect.disconnect();
            }
            else
            {
                MessageBox.Show("NO CONNECTION!!!");
            }


        }
        private void startListening()
        {
            while (threadCondition)
            {
                Byte[] bytes = new Byte[256];
                String data = null;

                Console.Write("Waiting for a connection... ");

                TcpClient client = _torChater.meListening.AcceptTcpClient();
                Console.WriteLine("Connected!");

                data = null;

                NetworkStream stream = client.GetStream();

                int i;

                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    ChatText.Text += data;
                    ChatText.Text += "\n";


                    data = data.ToUpper();

                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                    if (!threadCondition)
                        break;
                }
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
            if (_serverConnect.connection(recepientIP, recepientPORT))
            {
                _serverConnect.SendToServer(_mainName + " says: " + messageToSendBox.Text);
                _serverConnect.disconnect();
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
    }
}
