# Bluetooth TI Sensor Browser (Xamarin.Forms)

This sample uses the [Monkey.Robotics component](https://components.xamarin.com/view/Monkey.Robotics) in a [Xamarin.Forms](http://xamarin.com/forms) app to connect using Bluetooth LE to the [TI SensorTag](http://www.ti.com/ww/en/wireless_connectivity/sensortag/index.shtml) and access data from various sensors including temperature, accelerometer, pressure, humidity, and gyroscope (NOTE: code for parsing the barometer data is currently incomplete).

Here's a device shown next to the app running on an iPhone 6:

![screenshot](https://github.com/conceptdev/xamarin-forms-samples/raw/master/BluetoothTISensor/Screenshots/phone_plus_tag-sml.jpg "device and phone")

The complete screenflow for the iOS app (showing the Magnometer sensor) is below. The app works like a 'browser', first locating Bluetooth LE devices to connect to, then connecting and discovering services and their characteristics, finally allowing you to set and query specific characteristics. This app example has hardcoded references to the characteristics exposed by the [TI SensorTag](http://www.ti.com/ww/en/wireless_connectivity/sensortag/index.shtml).

![screenshot](https://github.com/conceptdev/xamarin-forms-samples/raw/master/BluetoothTISensor/Screenshots/ios-all-sml.png "iOS screen flow")

Here is the complete screenflow for the Android app (showing the Magnometer sensor):

![screenshot](https://github.com/conceptdev/xamarin-forms-samples/raw/master/BluetoothTISensor/Screenshots/android-all-sml.png "Android screen flow")

The Xamarin.Forms common code has a direct assembly reference to **Robotics.Mobile.Core.dll** in the **Components** directory, because components cannot be added to PCL projects.

The **Monkey.Robotics** component (and this app!) is in 'beta' and is provided to learn about using Bluetooth LE wiht Xamarin.Forms.

## Get a SensorTag

You can buy the TI SensorTag on [Amazon](http://www.amazon.com/INSTRUMENTS-CC2541DK-SENSOR-CC2541-2-4GHZ-BLUETOOTH/dp/B00HVOOP6S/), and they also have their own app for [iOS](https://itunes.apple.com/us/app/ti-sensortag/id552918064?mt=8), [Android](https://play.google.com/store/apps/details?id=com.ti.ble.sensortag), and [Windows](http://apps.microsoft.com/windows/en-us/app/ti-sensor-tag-reader/c5218f45-f779-41d9-a5ca-4624df94613d) (to play with and compare data outputs).

## Monkey.Robotics Source

The component that contains the Bluetooth LE abstraction for Xamarin.Forms is open-source, and is [available on github](https://github.com/xamarin/Monkey.Robotics). As noted on the [component page](https://components.xamarin.com/view/Monkey.Robotics) it is still in *beta*.