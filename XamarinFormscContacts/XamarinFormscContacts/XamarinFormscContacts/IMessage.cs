using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinFormscContacts
{
    public interface IMessage
    {
        void SendSMS(string ToNumber, string Message);
    }
}
