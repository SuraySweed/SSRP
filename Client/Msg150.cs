using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorChatClient
{
    class Msg150 : Msg
    {
        private List<Tuple<string, Int32>> _listOfAddresses;

        public Msg150(string data, List<Tuple<string, Int32>> listOfAddresses)
        {
            _data = data;
            _listOfAddresses = listOfAddresses;
            _messageCode = 150;
        }

        public List<Tuple<string, Int32>> getNextRoute()
        {
            List<Tuple<string, Int32>> newList = _listOfAddresses;
            newList.RemoveAt(newList.Count - 1);
            return newList;
        }

        public List<Tuple<string, Int32>> getCurrentRoute()
        {
            return _listOfAddresses;
        }

        public Tuple<string, Int32> getNextAdrressToSendTo()
        {
            return _listOfAddresses[_listOfAddresses.Count - 1];
        }

        public override string BuildMessageInString()
        {
            string msgToSend = "150|" + _data;

            for (int i = 0; i < _listOfAddresses.Count; i++)
            {
                msgToSend += "|" + _listOfAddresses[i].Item1.ToString() + "," + _listOfAddresses[i].Item2.ToString();
            }

            return msgToSend;
        }

    }
}
