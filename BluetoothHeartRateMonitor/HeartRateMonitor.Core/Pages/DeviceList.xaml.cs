using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Robotics.Mobile.Core.Bluetooth.LE;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace HeartRateMonitor
{	
	public partial class DeviceList : ContentPage
	{	
		IAdapter adapter;
		ObservableCollection<IDevice> devices;

		public DeviceList (IAdapter adapter)
		{
			InitializeComponent ();
			this.adapter = adapter;
			this.devices = new ObservableCollection<IDevice> ();
			listView.ItemsSource = devices;

			adapter.DeviceDiscovered += (object sender, DeviceDiscoveredEventArgs e) => {
				Device.BeginInvokeOnMainThread(() => {
					devices.Add (e.Device);
				});
			};

			adapter.ScanTimeoutElapsed += (sender, e) => {
				adapter.StopScanningForDevices(); // not sure why it doesn't stop already, if the timeout elapses... or is this a fake timeout we made?
				Device.BeginInvokeOnMainThread ( () => {
					IsBusy = false;
					DisplayAlert("Timeout", "Bluetooth scan timeout elapsed, no heart rate monitors were found", "OK");
				});
			};

			ScanHrmButton.Activated += (sender, e) => {
				InfoFrame.IsVisible = false;
				// this is the UUID for Heart Rate Monitors
				StartScanning (0x180D.UuidFromPartial());
			};
		}

		public void OnItemSelected (object sender, SelectedItemChangedEventArgs e) {
			if (((ListView)sender).SelectedItem == null) {
				return;
			}
			Debug.WriteLine (" xxxxxxxxxxxx  OnItemSelected " + e.SelectedItem.ToString ());
			IsBusy = false;
			StopScanning ();

			var device = e.SelectedItem as IDevice;
			var servicePage = new ServiceList(adapter, device);
			// load services on the next page
			Navigation.PushAsync(servicePage);

			((ListView)sender).SelectedItem = null; // clear selection
		}

		void StartScanning () {
			IsBusy = true;
			StartScanning (Guid.Empty);
		}
		void StartScanning (Guid forService) {
			if (adapter.IsScanning) {
				IsBusy = false;
				adapter.StopScanningForDevices();
				Debug.WriteLine ("adapter.StopScanningForDevices()");
			} else {
				devices.Clear();
				IsBusy = true;
				adapter.StartScanningForDevices(forService);
				Debug.WriteLine ("adapter.StartScanningForDevices("+forService+")");
			}
		}

		void StopScanning () {
			// stop scanning
			new Task( () => {
				if(adapter.IsScanning) {
					Debug.WriteLine ("Still scanning, stopping the scan");
					adapter.StopScanningForDevices();
					IsBusy = false;
				}
			}).Start();
		}
	}
}
