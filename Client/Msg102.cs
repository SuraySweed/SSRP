using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorChatClient
{
    class Msg102 : Msg
    {
        string _srcName;
        string _destName;

        public Msg102(string srcName, string dstName)
        {
            _data = null;
            _srcName = srcName;
            _destName = dstName;
        }

        public override string BuildMessageInString()
        {
            return "102|" + _srcName + "|" + _destName;
        }
    }
}
