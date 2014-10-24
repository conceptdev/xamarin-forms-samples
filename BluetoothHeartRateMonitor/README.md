# Bluetooth Heart Rate Monitor (Xamarin.Forms)

This sample uses the [Monkey.Robotics component](https://components.xamarin.com/view/Monkey.Robotics) in a [Xamarin.Forms](http://xamarin.com/forms) app to connect using Bluetooth LE to a Heart Rate Monitor (tested with the [Polar H7](http://www.polar.com/us-en/products/accessories/H7_heart_rate_sensor)) from both iOS and Android devices.


The Xamarin.Forms common code has a direct assembly reference to **Robotics.Mobile.Core.dll** in the **Components** directory, because components cannot be added to PCL projects.

The **Monkey.Robotics** component (and this app!) is in 'beta' and is provided to learn about using Bluetooth LE with Xamarin.Forms.

## Get a Heart Rate Monitor

You can buy a Bluetooth LE compatible monitor like the [Polar H7 from Polar direct](http://www.polar.com/us-en/products/accessories/H7_heart_rate_sensor) or on [Amazon for about $20 less](http://www.amazon.com/Polar-Bluetooth-Smart-Heart-Sensor/dp/B00NOHWTO6).

## Monkey.Robotics Source

The component that contains the Bluetooth LE abstraction for Xamarin.Forms is open-source, and is [available on github](https://github.com/xamarin/Monkey.Robotics). As noted on the [component page](https://components.xamarin.com/view/Monkey.Robotics) it is still in *beta*.