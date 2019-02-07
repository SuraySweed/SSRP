using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorChatClient
{
    public abstract class Msg
    {
        protected string _data;
        protected int _messageCode;

        public virtual string BuildMessageInString() { return null; }
        public string getData() { return _data; }
        public int getMessageCode() { return _messageCode; }
    }
}
