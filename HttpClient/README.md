HttpClient (Xamarin.Forms)
=========

This sample is a simple demo of `HttpClient` accessing a `Json` web service. The code is based on this [portable class library sample](http://bertt.wordpress.com/2013/03/19/using-geonames-webservices-from-portable-class-library-pcl/) - it retrieves data from [GeoNames](http://api.geonames.org/) and displays it in a `ListView`.

![screenshot](https://raw.githubusercontent.com/conceptdev/xamarin-forms-samples/master/HttpClient/Screenshots/iOS.png "iOS")

**NOTE:** if you get a build error related to HttpClient, do a *Clean* of the solution (and if that doesn't work, manually delete all the `obj` and `bin` folders).

NuGet Components
----------------

* **Xamarin.Forms** (of course)

* **Newtonsoft.Json** for parsing the Json response

* **Microsoft.Net.Http** for the HttpClient implementation


