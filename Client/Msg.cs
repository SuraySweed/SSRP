using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorChatClient
{
    abstract class Msg
    {
        protected string _data;

        public virtual string BuildMessageInString() { return null; }
        public string getData() { return _data; }
    }
}
