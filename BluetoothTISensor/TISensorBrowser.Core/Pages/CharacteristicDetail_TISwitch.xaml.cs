using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Robotics.Mobile.Core.Bluetooth.LE;
using System.Diagnostics;

namespace TISensorBrowser
{	
	/* 
	* this page is specifically for the on/off capabilities of the 
	* TI Sensor Tag characteristics. It should only be pushed by the listview
	* when the correct type of characteristic is detected.
	*/
	public partial class CharacteristicDetail_TISwitch : ContentPage
	{	
		ICharacteristic characteristic;

		public CharacteristicDetail_TISwitch (IAdapter adapter, IDevice device, IService service, ICharacteristic characteristic)
		{
			InitializeComponent ();
			this.characteristic = characteristic;
		}
	
		protected override async void OnAppearing ()
		{
			base.OnAppearing ();
			Debug.WriteLine ("ReadAsync: " + characteristic.Name);
			if (characteristic.CanRead) {
				var c = await characteristic.ReadAsync();
				UpdateDisplay(c);
				if (c.Value[0] == 0x00) {
					Enabled.IsToggled = false;
				} else {
					Enabled.IsToggled = true;
				}
			}
			// assign the event handler AFTER setting the intial value
			Enabled.Toggled += SwitchToggled;
		}
		protected override void OnDisappearing ()
		{
			base.OnDisappearing ();
			Enabled.Toggled -= SwitchToggled;
		}

		void UpdateDisplay (ICharacteristic c) {
			Debug.WriteLine ("UpdateDisplay");
			Name.Text = c.Name;
			ID.Text = c.ID.PartialFromUuid ();

			var s = (from i in c.Value
				select i.ToString ("X").PadRight(2, '0')).ToArray ();
			RawValue.Text = string.Join (":", s);
			StringValue.Text = c.StringValue;
		}

		async void SwitchToggled (object sender, ToggledEventArgs e) {
			Debug.WriteLine("Switching: " + characteristic.ID.PartialFromUuid ());
			if (e.Value) {
				if (characteristic.ID.PartialFromUuid () == "0xaa52") // gyroscope on/off
					characteristic.Write (new byte[] { 0x07 }); // enable XYZ axes 
				else if (characteristic.ID.PartialFromUuid () == "0xaa23") // humidity period
					characteristic.Write (new byte[] { 0x02 }); // 
				else
					characteristic.Write (new byte[] { 0x01 });
			} else { 
				// OFF
				characteristic.Write (new byte[] { 0x00 });
			}
			var c = await characteristic.ReadAsync();
			UpdateDisplay(c);
		}
	}
}

