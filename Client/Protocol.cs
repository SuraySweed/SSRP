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


        public dynamic handleRecvMsg(string rcvdMSG)
        {
            string[] splitedMSG = rcvdMSG.Split('|');

            if (splitedMSG[0] == "201")// getting welcome message and our port
            {
                List<string> listOfInformation = new List<string>();
                listOfInformation.Add(splitedMSG[1]);
                string port = (splitedMSG[2].Split('\0'))[0];
                listOfInformation.Add(port);

                return listOfInformation;
            }
            else if (splitedMSG[0] == "203") // getting other dude's address
            {
                if (splitedMSG.Length == 2)
                {
                    return null;
                }
                else
                {
                    List<string> recvMsg = new List<string>();
                    recvMsg.Add(splitedMSG[1]);
                    string port = (splitedMSG[2].Split('\0'))[0];
                    recvMsg.Add(port);

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
