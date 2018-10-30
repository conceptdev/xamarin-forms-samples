using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Messaging;
using Android;
using Android.Content.PM;

namespace XamarinFormscContacts
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();


        }


       // const string permission = Manifest.Permission.SendSms;
        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                //if(CheckSelfPermission(permission) == (int)Permission.Granted)
                DependencyService.Get<IMessage>().SendSMS("9500192219", "Test Message");

                var smsManager = CrossMessaging.Current.SmsMessenger;
                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }




            //switch (Device.RuntimePlatform)
            //{
            //    case Device.UWP:
            //        var selectContact = await DependencyService.Get<IContacts>().GetDeviceContactsAsync();
            //        listContact.ItemsSource = selectContact;
            //        break;

            //    case Device.Android:

            //        //var smsManager = CrossMessaging.Current.SmsMessenger;
            //        //if (smsManager.CanSendSmsInBackground)
            //        //{
            //        //    smsManager.SendSmsInBackground("9500192219", "test sms");
            //        //}

            //        //DependencyService.Get<IMessage>().SendSMS("9500192219", "Test Message");
            //        //var ContactList = await DependencyService.Get<IContacts>().GetDeviceContactsAsync();
            //        //listContact.ItemsSource = ContactList;
            //        break;
            //    default:

            //        break;
            //}

        }


    }

}
