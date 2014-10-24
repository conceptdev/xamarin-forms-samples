using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Robotics.Mobile.Core.Bluetooth.LE;
using System.Diagnostics;
using System.Linq;

namespace TISensorBrowser
{	
	public partial class CharacteristicDetail : ContentPage
	{	
		IAdapter adapter;
		IDevice device;
		IService service; 
		ICharacteristic characteristic;

		public CharacteristicDetail (IAdapter adapter, IDevice device, IService service, ICharacteristic characteristic)
		{
			InitializeComponent ();
			this.characteristic = characteristic;

			if (characteristic.CanUpdate) {
				characteristic.ValueUpdated += (s, e) => {
					Debug.WriteLine("characteristic.ValueUpdated");
					Device.BeginInvokeOnMainThread( () => {
						UpdateDisplay(characteristic);
					});
				};
				characteristic.StartUpdates();
			}
		}

		protected override async void OnAppearing ()
		{
			base.OnAppearing ();
		
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
			}
		}
		void UpdateDisplay (ICharacteristic c) {
			Name.Text = c.Name;
			//ID.Text = c.ID.ToString();
			ID.Text = c.ID.PartialFromUuid ();

			var s = (from i in c.Value
					select i.ToString ("X").PadRight(2, '0')).ToArray ();
			RawValue.Text = string.Join (":", s);

			StringValue.Text = c.StringValue;
			StringValue.TextColor = Color.Default;
		}
	}
}

