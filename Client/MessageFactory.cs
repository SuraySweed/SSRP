﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace TorChatClient
{
    class MessageFactory
    {

        public Msg connectToServerMsg(string nickName)
        {
            Msg msg100 = new Msg100(nickName);
            return msg100;
        }

        public Msg sendRecepientNameMsg(string myName, string recepientName)
        {
            Msg msg102 = new Msg102(myName, recepientName);
            return msg102;
        }

        public Msg PeersMessages(string msg, List<Tuple<string, Int32>> listOfAddresses)
        {
            Msg msgToSend = null;

            if (listOfAddresses.Count == 0)
            {
                msgToSend = new Msg151(msg);
            }
            else
            {
                msgToSend = new Msg150(msg, listOfAddresses);
            }

            return msgToSend;
        }

        public Msg handleRecvMsg(string rcvdMSG)
        {
            string[] splitedMSG = rcvdMSG.Split('|');

            if (splitedMSG[0] == "201")// getting welcome message and our port
            {
                string ip = (splitedMSG[3].Split('\0'))[0];

                Msg msg201 = new Msg201(Int32.Parse(splitedMSG[2]), IPAddress.Parse(ip), splitedMSG[1]);

                return msg201;
            }
            else if (splitedMSG[0] == "203") // getting the route to the other dude
            {
                Msg msg203;
                if (splitedMSG.Length == 2)
                {
                    msg203 = null;
                }
                else
                {
                    List<Tuple<string, Int32>> recvMsg = new List<Tuple<string, Int32>>();

                    for (int i = 1; i < splitedMSG.Length - 1; i++)
                    {
                        Tuple<string, Int32> tuple = new Tuple<string, int>(splitedMSG[i].Split(',')[0], Int32.Parse(splitedMSG[i].Split(',')[1]));

                        recvMsg.Add(tuple);
                    }

                    msg203 = new Msg203(recvMsg);
                }
                return msg203;
            }
            else if (splitedMSG[0] == "150") // forward
            {
                List<Tuple<string, Int32>> addressesList = new List<Tuple<string, int>>();
                List<string> theMsg = new List<string>(splitedMSG);

                foreach (string add in theMsg.GetRange(2, theMsg.Count - 2))
                {
                    string[] IPPORT = add.Split(',');
                    addressesList.Add(new Tuple<string, Int32>(IPPORT[0], Int32.Parse(IPPORT[1])));
                }

                Msg msg150 = new Msg150(splitedMSG[1], addressesList);

                return msg150;
            }
            else if (splitedMSG[0] == "151")// got a message
            {
                Msg msg151 = new Msg151(splitedMSG[1]);
                
                return msg151;
            }
            else
            {
                return null;
            }
        }

    }
}
