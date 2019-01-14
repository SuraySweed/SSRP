using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorChatClient
{
    public class Protocol
    {
        public string connectToServerMsg(string nickName)
        {
            return "100|" + nickName;
        }

        public string sendRecepientNameMsg(string recepientName)
        {
            return "102|" + recepientName;
        }

        public string messageToBeSent(string msg, List<Tuple<string, Int32>> listOfAddresses)
        {
            string msgToSend = "150|" + msg;

            for(int i=0;i<listOfAddresses.Count; i++)
            {
                msgToSend += listOfAddresses[i].ToString() + "|";
            }

            return msgToSend;
        }


        public dynamic handleRecvMsg(string rcvdMSG)
        {
            string[] splitedMSG = rcvdMSG.Split('|');

            if (splitedMSG[0] == "201")// getting welcome message and our port
            {
                List<string> listOfInformation = new List<string>();
                listOfInformation.Add(splitedMSG[1]);
                listOfInformation.Add(splitedMSG[2]);
                string ip = (splitedMSG[3].Split('\0'))[0];
                listOfInformation.Add(ip);

                return listOfInformation;
            }
            else if (splitedMSG[0] == "203") // getting the route to the other dude
            {
                if (splitedMSG.Length == 2)
                {
                    return null;
                }
                else
                {
                    List<Tuple<string,Int32>> recvMsg = new List<Tuple<string, Int32>>();
                    
                    for (int i = 1; i < splitedMSG.Length - 1; i++)
                    {
                        Tuple<string, Int32> tuple = new Tuple<string, int>(splitedMSG[i].Split(',')[0], Int32.Parse(splitedMSG[i].Split(',')[1]));
                        
                        recvMsg.Add(tuple);
                    }

                    return recvMsg;
                }
            }
            else if (splitedMSG[0] == "205")
            {
                return 0;
            }
            else
            {
                return 0;
            }
        }
    }
}
