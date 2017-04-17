Xamarin.Forms Demo Apps
===========

Many of the Xamarin.Forms samples in this repo are discussed in **Intro_To_Xamarin.Forms.pdf** document.

* **EmployeeDirectoryXaml** - port of the existing Xamarin Employee Directory sample... (uses PCLStorage Nuget). Still in-progress.

* **Evolve13** - port of the Xamarin Evolve conference app used *last year* (uses SQLite.Net-PCL Nuget), using the MasterDetailPage.

* **FormsBasics** - simple explanations of basic Xamarin.Forms concepts like views, controls and layouts.

* **HttpClient** - simple example of using HttpClient for network communication with Xamarin.Forms.

* **PlatformSpecific** - simple examples of how to do various platform-specific operations (in code and in XAML).

* **RestaurantGuide** - how to render HTML using Razor templates and the Xamarin.Forms `WebView` control.

* **Roget1911** - port of an old Silverlight sample, demonstrating a deep navigation structure generated from parsed XML files (uses PCLStorage Nuget).

* **Todo** - simple todo list application that demonstrates how to load a pre-populated database and then read and write data to it (uses SQLite.Net-PCL Nuget). Also shows how to add platform-specific code to a Xamarin.Forms app, by implementing the text-to-speech engines on iOS and Android.


* **TodoAzure** - same todo list application logic XAML, and using [Windows Azure Mobile Services](http://windowsazure.com) for cloud data storage (Windows Azure Nuget/Component).

* **TodoAzureSync** - same todo list application logic XAML, and using [Windows Azure Mobile Services](http://windowsazure.com) offline sync component for cloud data storage (Windows Azure Nuget/Component).

* **TodoL10nResx** - todo list that is localized/globalized using RESX files.

* **TodoL10nVernacular** - todo list that is localized/globalized using [Vernacular](http://github.com/rdio/vernacular/).

* **TodoMvvm** - same todo list application logic, demonstrating Mvvm architecture with Xamarin.Forms. Uses data-binding and the command pattern to wire up model, view and view model objects (uses SQLite.Net-PCL Nuget).

* **TodoParse** - similar to the Azure examples but uses [Parse](http://parse.com) instead.

* **TodoXaml** - same todo list application logic, but using XAML to declaratively build the screen layouts (rather than C# code, uses SQLite.Net-PCL Nuget). 

## Unfinished

* **TicTacForms** - simple tic tac toe game ~ still under development...



Nugets used in this repo:

[Xamarin.Forms](http://www.nuget.org/packages/Xamarin.Forms/)

[Xamarin.Forms.Maps](http://www.nuget.org/packages/Xamarin.Forms.Maps/)

[SQLite.NET-PCL](http://www.nuget.org/packages/SQLite.Net-PCL/)

[PCLStorage](http://www.nuget.org/packages/PCLStorage/0.9.4)

Also the Azure ones, and the Parse Xamarin Store Component.


Other Interesting Repos
-----------------------

### [XForms](https://github.com/XForms/Xamarin-Forms-Labs)
Community-driven controls and extensions for Xamarin.Forms

### [acr-xamarin-forms](https://github.com/aritchie/acr-xamarin-forms#acr-xamarin-forms)
Another source of controls written by Allan Ritchie

