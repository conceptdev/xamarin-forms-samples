using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinFormscContacts
{
    public interface IContacts
    {
        Task<List<ContactLists>> GetDeviceContactsAsync();
    }
}
