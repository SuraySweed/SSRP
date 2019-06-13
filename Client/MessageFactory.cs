using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace TorChatClient
{
    class MessageFactory
    {
        RSA _encryptionHelper = null;

        public MessageFactory(RSA rsa)
        {
            _encryptionHelper = rsa;
        }

        public Msg Msg
        {
            get => default(Msg);
            set
            {
            }
        }

        public Msg connectToServerMsg(string nickName)
        {
            Msg msg100 = new Msg100(nickName, _encryptionHelper.getPublicKeyInString());
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
            string[] splitedMSG = rcvdMSG.Split(new string[] { "||||" }, StringSplitOptions.None);

            if (splitedMSG[0] == "201")// getting welcome message and our port
            {
                string ip = (splitedMSG[3].Split('\0'))[0];

                Msg msg201 = new Msg201(Int32.Parse(splitedMSG[2]), IPAddress.Parse(ip), splitedMSG[1]);

                return msg201;
            }
            else if (splitedMSG[0] == "203") // getting the route to the other dude ***and the keys***
            {
                Msg msg203;
                if (splitedMSG.Length == 2)
                {
                    msg203 = null;
                }
                else
                {
                    List<Tuple<string, Int32>> recvMsg = new List<Tuple<string, Int32>>();
                    List<string> keysList = new List<string>();

                    for (int i = 1; i < splitedMSG.Length - 1; i++)
                    {
                        string[] innerInfo = splitedMSG[i].Split(',');

                        Tuple<string, Int32> tuple = new Tuple<string, int>(innerInfo[0], Int32.Parse(innerInfo[1]));

                        keysList.Add(innerInfo[2]);
                        recvMsg.Add(tuple);
                    }

                    msg203 = new Msg203(recvMsg, keysList);
                }
                return msg203;
            }
            else if (splitedMSG[0] == "150") // forward
            {
                //byte[] bytes = Encoding.UTF8.GetBytes(splitedMSG[1]);
                int routesLeft = 0;
                String[] s = splitedMSG[1].Split('-');
                byte[] a = new byte[s.Length -1];
                for (int i = 0; i < s.Length - 1; i++)
                {
                    a[i] = Convert.ToByte(s[i], 16);
                }

                List<byte[]> encryptedListToDycrypt = new List<byte[]>();
                string decryptedMsg = "150||||";

                for (int i = 0; i < a.Length / 256; i++) 
                {
                    encryptedListToDycrypt.Add(a.Skip(256 * i).Take(256).ToArray());
                }
                for (int i = 0; i < encryptedListToDycrypt.Count; i++)
                {
                    encryptedListToDycrypt[i] = _encryptionHelper.Decrypt(encryptedListToDycrypt[i]); //decrypted parts
                    decryptedMsg += Encoding.UTF8.GetString(encryptedListToDycrypt[i]);
                }

                List<Tuple<string, Int32>> addressesList = new List<Tuple<string, int>>();
                List<string> theMsg = new List<string>(decryptedMsg.Split(new string[] { "||||" }, StringSplitOptions.None));

                foreach (string add in theMsg.GetRange(2, theMsg.Count - 2))
                {
                    string[] IPPORT = add.Split(',');
                    addressesList.Add(new Tuple<string, Int32>(IPPORT[0], Int32.Parse(IPPORT[1])));
                    routesLeft = Int32.Parse(IPPORT[2]);
                }

                for (int i = 0; i < routesLeft; i++)
                {
                    addressesList.Add(null);
                }

                List<byte> list1 = new List<byte>();
                for (int k = 0; k < encryptedListToDycrypt.Count; k++)
                {
                    list1.AddRange(encryptedListToDycrypt[k]);
                }


                byte[] msgToForward = list1.ToArray();

                msgToForward = msgToForward.Take(msgToForward.Length - msgToForward.Length % 256).ToArray();


                Msg msg150 = new Msg150(BitConverter.ToString(msgToForward) + "-", addressesList);
                
                return msg150;
            }
            else if (splitedMSG[0] == "151")// got a message
            {
                
                String[] s = splitedMSG[1].Split('-');
                byte[] a = new byte[s.Length - 1];
                for (int i = 0; i < s.Length - 1; i++)
                {
                    a[i] = Convert.ToByte(s[i], 16);
                }
                
                byte[] b = _encryptionHelper.Decrypt(a);

                Msg msg151 = new Msg151(Encoding.UTF8.GetString(b));
                
                return msg151;
            }
            else
            {
                return null;
            }
        }

    }
}
