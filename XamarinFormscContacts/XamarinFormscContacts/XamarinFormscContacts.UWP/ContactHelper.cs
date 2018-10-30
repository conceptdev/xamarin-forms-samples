using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinFormscContacts;
using XamarinFormscContacts.UWP;
using Xamarin.Forms.Platform;
using Xamarin.Forms;
using Windows.ApplicationModel.Contacts;
using System.Diagnostics;

[assembly: Xamarin.Forms.Dependency(typeof(ContactHelper))]
namespace XamarinFormscContacts.UWP
{
    public class ContactHelper: IContacts
    {
        public async Task<List<ContactLists>> GetDeviceContactsAsync()
        {
            List<ContactLists> selectedContact = new List<ContactLists>();
            var contactPicker = new ContactPicker();
            contactPicker.DesiredFieldsWithContactFieldType.Add(ContactFieldType.PhoneNumber);
            Contact contact = await contactPicker.PickContactAsync();

            selectedContact.Add(new ContactLists() { DisplayName = contact.SortName, ContactNumber = contact.YomiDisplayName });
            return selectedContact;
        }

       
    }
}
