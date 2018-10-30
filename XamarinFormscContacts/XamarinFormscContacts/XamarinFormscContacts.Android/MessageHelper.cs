using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using XamarinFormscContacts.Droid;
using Android.Telephony;

[assembly:Dependency(typeof(MessageHelper))]
namespace XamarinFormscContacts.Droid
{
    class MessageHelper : IMessage
    {

        SmsManager smsManager;
        BroadcastReceiver rece;
        public void SendSMS(string ToNumber, string Message)
        {

            smsManager = SmsManager.Default;
            smsManager.SendTextMessage("9500192219", null, "Test Message", null, null);
            //rece = new BroadcastReceiver();  

            sample s = new sample();
           
        }
    }
    [BroadcastReceiver]
    [IntentFilter(new[] { "android.provider.Telephony.SMS_RECEIVED" })]
    class sample: BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.HasExtra("pdus"))
            {
                var smsArray = (Java.Lang.Object[])intent.Extras.Get("pdus");
                string address = "";
                string message = "";
                foreach (var item in smsArray)
                {
                    var sms = SmsMessage.CreateFromPdu((byte[])item);
                    message += sms.MessageBody;
                    address = sms.OriginatingAddress;
                    Toast.MakeText(context, address + message, ToastLength.Long).Show();
                }
            }
        }
    }
}