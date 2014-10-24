using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;
using Robotics.Mobile.Core.Bluetooth.LE;
using System.Diagnostics;

namespace HeartRateMonitor
{	
	public partial class CharacteristicDetail_Hrm : ContentPage
	{	
		ICharacteristic characteristic;

		//EventHandler<CharacteristicReadEventArgs> valueUpdatedHandler;

		public CharacteristicDetail_Hrm (IAdapter adapter, IDevice device, IService service, ICharacteristic characteristic)
		{
			InitializeComponent ();
			this.characteristic = characteristic;

			Title = characteristic.Name;

			if (characteristic.CanUpdate) {
				characteristic.ValueUpdated += (s, e) => {
					Debug.WriteLine("characteristic.ValueUpdated");
					Device.BeginInvokeOnMainThread( () => {
						UpdateDisplay(characteristic);
					});
					IsBusy = false; // only spin until the first result is received
				};
				IsBusy = true;
				characteristic.StartUpdates();
			}
		}

		protected override async void OnAppearing ()
		{
			InitializeComponent ();
			this.characteristic = characteristic;

			if (characteristic.CanRead) {
				var c = await characteristic.ReadAsync();
				UpdateDisplay(c);
			}
		}
		protected override void OnDisappearing() 
		{
			base.OnDisappearing();
			if (characteristic.CanUpdate) {
				characteristic.StopUpdates();
				//characteristic.ValueUpdated -= valueUpdatedHandler;
			}
		}

		void UpdateDisplay (ICharacteristic c) {
			Debug.WriteLine ("UpdateDisplay");
			Name.Text = c.Name;
			ID.Text = c.ID.PartialFromUuid ();

			var s = (from i in c.Value
				select i.ToString ("X").PadRight(2, '0')).ToArray ();
			RawValue.Text = string.Join (":", s);
			StringValue.Text = c.StringValue;


			if (c.ID == 0x2A37.UuidFromPartial ()) {
				// heart rate
				StringValue.Text = DecodeHeartRateCharacteristicValue (c.Value);
				StringValue.TextColor = Color.Red;
			} else if (c.ID == 0x2A38.UuidFromPartial ()) { 
				StringValue.Text = DecodeHeartMonitorPosition (c.Value);
				StringValue.TextColor = Color.Olive;
			} else {
				StringValue.Text = c.StringValue;
				StringValue.TextColor = Color.Default;
			}


		}


		string DecodeHeartRateCharacteristicValue(byte[] data) {
			ushort bpm = 0;
			if ((data [0] & 0x01) == 0) {
				bpm = data [1];
			} else {
				bpm = (ushort)data [1];
				bpm = (ushort)(((bpm >> 8) & 0xFF) | ((bpm << 8) & 0xFF00));
			}
			return bpm.ToString () + " bpm";
		}


		string DecodeHeartMonitorPosition (byte[] data) {
			var position = data[0];
			var locationString = "-";
			Debug.WriteLine("----------------------position:" + position);
			// https://developer.apple.com/library/mac/samplecode/HeartRateMonitor/Listings/HeartRateMonitor_HeartRateMonitorAppDelegate_m.html
			switch (position) {
			case 0:
				locationString = @"Other";
				break;
			case 1:
				locationString = @"Chest";
				break;
			case 2:
				locationString = @"Wrist";
				break;
			case 3:
				locationString = @"Finger";
				break;
			case 4:
				locationString = @"Hand";
				break;
			case 5:
				locationString = @"Ear Lobe";
				break;
			case 6: 
				locationString = @"Foot";
				break;
			default:
				locationString = @"Reserved";
				break;
			}
			return locationString;
		}
	}
}

