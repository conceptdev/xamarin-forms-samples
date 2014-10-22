using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Robotics.Mobile.Core.Bluetooth.LE;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace TISensorBrowser
{	
	public partial class ServiceList : ContentPage
	{	
		IAdapter adapter;
		IDevice device;

		ObservableCollection<IService> services;

		public ServiceList (IAdapter adapter, IDevice device)
		{
			InitializeComponent ();
			this.adapter = adapter;
			this.device = device;
			this.services = new ObservableCollection<IService> ();
			listView.ItemsSource = services;

			// when device is connected
			adapter.DeviceConnected += (s, e) => {
				device = e.Device; // do we need to overwrite this?

				// when services are discovered
				device.ServicesDiscovered += (object se, EventArgs ea) => {
					Debug.WriteLine("device.ServicesDiscovered");
					//services = (List<IService>)device.Services;
					if (services.Count == 0)
						Device.BeginInvokeOnMainThread(() => {
							foreach (var service in device.Services) {
								services.Add(service);
							}
						});
				};
				Device.BeginInvokeOnMainThread (() => {
					IsBusy = false;
				});
				// start looking for services
				device.DiscoverServices ();

			};
			// TODO: add to IAdapter first
			//adapter.DeviceFailedToConnect += (sender, else) => {};

			DisconnectButton.Activated += (sender, e) => {
				adapter.DisconnectDevice (device);
				Navigation.PopToRootAsync(); // disconnect means start over
			};
		}


		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			if (services.Count == 0) {
				Debug.WriteLine ("No services, attempting to connect to device");
				IsBusy = true;
				// start looking for the device
				adapter.ConnectToDevice (device); 
			}
		}
		public void OnItemSelected (object sender, SelectedItemChangedEventArgs e) {
			if (((ListView)sender).SelectedItem == null) {
				return;
			}

			var service = e.SelectedItem as IService;
			var characteristicsPage = new CharacteristicList(adapter, device, service);
			Navigation.PushAsync(characteristicsPage);

			((ListView)sender).SelectedItem = null; // clear selection
		}
	}
}

