using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Diagnostics;
using Robotics.Mobile.Core.Bluetooth.LE;

namespace TISensorBrowser
{	
	public partial class CharacteristicDetail_TISensor : ContentPage
	{	
		ICharacteristic characteristic;

		double gyro_calX, gyro_calY, gyro_calZ;
		double magno_calX, magno_calY, magno_calZ;
		bool gyro_calibrated = false, magno_calibrated = false;

		EventHandler<CharacteristicReadEventArgs> valueUpdatedHandler;

		public CharacteristicDetail_TISensor (IAdapter adapter, IDevice device, IService service, ICharacteristic characteristic)
		{
			InitializeComponent ();
			this.characteristic = characteristic;

			Title = characteristic.Name;

			if (Title.Contains ("Keys Data")) {
				InstructionsText.Text = "Press the two buttons on the TI Sensor to see the data generated below.";
			}
		}

		protected override async void OnAppearing ()
		{
			base.OnAppearing ();

			if (characteristic.CanUpdate) {
				// NOTIFY-UPDATE
				valueUpdatedHandler = (s, e) => {
					Debug.WriteLine ("-- _valueUpdatedHandler: " + e.Characteristic.Value);
					Device.BeginInvokeOnMainThread (() => {
						UpdateDisplay (e.Characteristic);
						SensorValue.Text = Decode (e.Characteristic);
					});
				};
				characteristic.ValueUpdated += valueUpdatedHandler;

				// request the value to be read
				characteristic.StartUpdates ();
			} else if (characteristic.CanRead) {
				var c = await characteristic.ReadAsync();
				UpdateDisplay(c);
			}
		}
		protected override void OnDisappearing() 
		{
			base.OnDisappearing();
			if (characteristic.CanUpdate) {
				characteristic.StopUpdates();
				characteristic.ValueUpdated -= valueUpdatedHandler;
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
		}


		public string Decode (ICharacteristic _characteristic) {
			string output = "";
			double progress = -1; 
			double xb = 0, yb = 0, zb = 0;
			//
			// TI SensorTag hardcoded read/write
			//
			// http://processors.wiki.ti.com/index.php/SensorTag_User_Guide
			//
			Debug.WriteLine ("PopulateValueInfo switch for : " + _characteristic.ID.PartialFromUuid ());
			var sensorData = _characteristic.Value;
			if (_characteristic.ID.PartialFromUuid () == "0xaa01") {
				// Temperature sensorTMP006 - works
				var ambientTemperature = BitConverter.ToUInt16 (sensorData, 2) / 128.0;
				double Tdie = ambientTemperature + 273.15;


				// http://sensortag.codeplex.com/SourceControl/latest#SensorTagLibrary/SensorTagLibrary/Source/Sensors/IRTemperatureSensor.cs
				double Vobj2 = BitConverter.ToInt16 (sensorData, 0);
				Vobj2 *= 0.00000015625;

				double S0 = 5.593E-14;
				double a1 = 1.75E-3;
				double a2 = -1.678E-5;
				double b0 = -2.94E-5;
				double b1 = -5.7E-7;
				double b2 = 4.63E-9;
				double c2 = 13.4;
				double Tref = 298.15;
				double S = S0 * (1 + a1 * (Tdie - Tref) + a2 * Math.Pow ((Tdie - Tref), 2));
				double Vos = b0 + b1 * (Tdie - Tref) + b2 * Math.Pow ((Tdie - Tref), 2);
				double fObj = (Vobj2 - Vos) + c2 * Math.Pow ((Vobj2 - Vos), 2);
				double tObj = Math.Pow (Math.Pow (Tdie, 4) + (fObj / S), .25);

				tObj -= 273.15;

				output = "ambient: " + Math.Round(ambientTemperature,1) + "\nIR: " + Math.Round(tObj,1) + " C";

				// for "graphs"
				xb = tObj * 5;
				yb = ambientTemperature * 5;
			} else if (_characteristic.ID.PartialFromUuid () == "0xaa11") {
				// Accelerometer sensorKXTJ9
				int x = sensorData [0];
				int y = sensorData [1];
				int z = sensorData [2];

				//					x = (byte)((x * 0x0202020202 & 0x010884422010) % 1023); 
				//					y = (byte)((y * 0x0202020202 & 0x010884422010) % 1023); 
				//					z = (byte)((z * 0x0202020202 & 0x010884422010) % 1023); 

				const double KXTJ9_RANGE = 4.0;

				double scaledX = (x * 1.0) / (256.0 / KXTJ9_RANGE);
				double scaledY = (y * 1.0) / (256.0 / KXTJ9_RANGE) * -1; // Orientation of sensor on board means we need to swap Y (multiplying with -1)
				double scaledZ = (z * 1.0) / (256.0 / KXTJ9_RANGE);

				output = String.Format ("scaled: {0}, {1}, {2} xyz", Math.Round(scaledX,2), Math.Round(scaledY,2), Math.Round(scaledZ,2));

			} else if (_characteristic.ID.PartialFromUuid () == "0xaa21") {
				// Humidity sensorSHT21 - works
				int a = BitConverter.ToUInt16 (sensorData, 2);
				a = a - (a % 4);
				double humidity = (-6f) + 125f * (a / 65535f);

				//					int t = BitConverter.ToInt16 (sensorData, 0);
				var t = (sensorData[0] & 0xff) | ((sensorData[1] << 8) & 0xff00); // iono what this sensor is returning :-(

				output = "humidity: " + Math.Round(humidity,1) + "%rH\ntemp: " + Math.Round(t / 1000.0, 1) + "C"; // HACK /1000

			} else if (_characteristic.ID.PartialFromUuid () == "0xaa31") {
				// Magnometer sensorMAG3110

				int x1 = BitConverter.ToInt16 (sensorData, 0);
				int y1 = BitConverter.ToInt16 (sensorData, 2);
				int z1 = BitConverter.ToInt16 (sensorData, 4);

				const double MAG3110_RANGE = 2000.0;
				// calculate acceleration, unit G, range -2, +2
				double x = x1 * (MAG3110_RANGE / 65536f) * -1; //Orientation of sensor on board means we need to swap X (multiplying with -1)
				double y = y1 * (MAG3110_RANGE / 65536f) * -1; //Orientation of sensor on board means we need to swap Y (multiplying with -1)
				double z = z1 * (MAG3110_RANGE / 65536f);

				if (!magno_calibrated) {
					magno_calX = x;
					magno_calY = y;
					magno_calZ = z;
					magno_calibrated = true;
				}

				output = String.Format ("heading: {0}, {1}, {2} /nmag uT", 
					Math.Round(x - magno_calX,1),
					Math.Round(y - magno_calY,1),
					Math.Round(z - magno_calZ,1));
				// TODO: http://cache.freescale.com/files/sensors/doc/app_note/AN4248.pdf?fpsp=1

				// for "graphs"
				xb = 300 + 10 * (x - magno_calX);
				yb = 300 + 10 * (y - magno_calY);
				zb = 300 + 10 * (z - magno_calZ);

			} else if (_characteristic.ID.PartialFromUuid () == "0xaa41") {
				// Barometer
				// TODO: !!!!!!!!!









			} else if (_characteristic.ID.PartialFromUuid () == "0xaa51") {
				// Gyroscope
				int x1 = BitConverter.ToInt16 (sensorData, 0);
				int y1 = BitConverter.ToInt16 (sensorData, 2);
				int z1 = BitConverter.ToInt16 (sensorData, 4);

				const double IMU3000_RANGE = 500.0;

				double x = (x1 * 1.0) / (65536 / IMU3000_RANGE);
				double y = (y1 * 1.0) / (65536 / IMU3000_RANGE);
				double z = (z1 * 1.0) / (65536 / IMU3000_RANGE); 

				if (!gyro_calibrated) {
					gyro_calX = x;
					gyro_calY = y;
					gyro_calZ = z;
					gyro_calibrated = true;
				}

				output = String.Format ("rotation: {0}, {1}, {2} /nxyz degrees/sec", 
					Math.Round(x - gyro_calX,1),
					Math.Round(y - gyro_calY,1),
					Math.Round(z - gyro_calZ,1));

				// for "graphs" 
				xb = 300 + (x - gyro_calX);
				yb = 300 + (y - gyro_calY);
				zb = 300 + (y - gyro_calZ);
			} else if (_characteristic.ID.PartialFromUuid () == "0xffe1") {
				// Smart Keys: Bit 2 - side key, Bit 1 - right key, Bit 0 – left key - works
				var b = ((int)_characteristic.Value[0]) % 4;
				switch (b) {
				case 1:
					output = "Right button";
					break;
				case 2:
					output = "Left button";
					break;
				case 3:
					output = "Both buttons";
					break;
				default:
					output = "Neither button";
					break;
				}
				output += " " + b;
			}

			if (xb > 0) {
				AbsoluteLayout.SetLayoutBounds(Box1, new Rectangle(10, 10, xb, 10));
				AbsoluteLayout.SetLayoutBounds(Box2, new Rectangle(10, 30, yb, 10));
				AbsoluteLayout.SetLayoutBounds(Box3, new Rectangle(10, 50, zb, 10));
			}
			return output;
		}
	}
}

