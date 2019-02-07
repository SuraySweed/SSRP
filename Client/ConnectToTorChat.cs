﻿using System;
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
    public partial class ConnectToTorChat : Form
    {
        public PortAndIPOfServer _PortAndIPOfServer = new PortAndIPOfServer();
        public TCPConnection _connection = new TCPConnection();
        public Listener meListening = null;
        private MessageFactory _MessageFactory = new MessageFactory();
        
        public Protocol protocol = new Protocol();
        
        public ConnectToTorChat()
        {
            InitializeComponent();
        }

        private void TorChater_Load(object sender, EventArgs e)
        {


        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            if(meListening != null)
                meListening.StopListening();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (_connection.establishConnection(_PortAndIPOfServer.GetServerIP(), _PortAndIPOfServer.GetServerPort()))
            {
                if (nameBox.Text != null && nameBox.Text != "")
                {
                    _connection.SendData(_MessageFactory.connectToServerMsg(nameBox.Text).BuildMessageInString());

                    Msg msgRecved = _MessageFactory.handleRecvMsg(_connection.ReceiveData());
                    Msg201 msg201 = (Msg201)msgRecved;
                    
                    meListening = new Listener(msg201.IP, msg201.Port);
                    MessageBox.Show(msg201.getData());
                 
                    ConnectToTorChat torChater = this as ConnectToTorChat;
                    ChatGUI chatForm = new ChatGUI(ref torChater, nameBox.Text);

                    this.Hide();
                    chatForm.Show();
                }
                else
                {
                    MessageBox.Show("YOU DIDN'T TYPE YOUR NAME!!!");
                }

                _connection.disconnect();
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
    }
}