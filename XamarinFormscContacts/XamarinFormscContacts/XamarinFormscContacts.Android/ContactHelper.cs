using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using XamarinFormscContacts.Droid;
using Android.Provider;
using Android.Net;

[assembly: Dependency(typeof(ContactHelper))]
namespace XamarinFormscContacts.Droid
{
    class ContactHelper : IContacts
    {
        public async Task<List<ContactLists>>  GetDeviceContactsAsync()
        {
            ContactLists selectedContact = new ContactLists();
            List<ContactLists> contactList = new List<ContactLists>();
            var uri =  ContactsContract.CommonDataKinds.Phone.ContentUri;
            string[] projection =  { ContactsContract.Contacts.InterfaceConsts.Id, ContactsContract.Contacts.InterfaceConsts.DisplayName, ContactsContract.CommonDataKinds.Phone.Number };
            var cursor =  Xamarin.Forms.Forms.Context.ContentResolver.Query(uri, projection, null, null, null);
            if (cursor.MoveToFirst())
            {
                do
                {
                    contactList.Add(new ContactLists()
                    {
                        DisplayName =  cursor.GetString(cursor.GetColumnIndex(projection[1]))
                    });
                } while (cursor.MoveToNext());
            }
            return contactList;
        }
        private object ManagedQuery(Android.Net.Uri uri, string[] projection, object p1, object p2, object p3)
        {
            throw new NotImplementedException();
        }
    }

}