TodoL10nVernacular (Xamarin.Forms)
=============

This sample uses [Vernacular by rdio](https://github.com/rdio/vernacular) to localize a Xamarin.Forms application for iOS and Android.

It has the [`Vernacular.Catalog.XamarinForms`](https://github.com/rdio/vernacular/tree/master/Vernacular.Catalog) assembly embedded (built against Xamarin.Forms 1.2.2.6238) as well as `Vernacular.Catalog.XamarinForms.Android` because a speciail **ResourceCatalog** implementation is required for Android. You can get that [code](https://github.com/rdio/vernacular/tree/master/Vernacular.Catalog) from github and build it yourself against newer releases of Xamarin.Forms.

Refer to [these slides on Vernacular](https://dl.dropboxusercontent.com/u/10397738/vernacular.pdf) and this [MonkeySpace talk video](http://vimeo.com/album/2142123/video/55647621) by [@abock](https://twitter.com/abock) for more info on Vernacular.

**SEE ALSO** the [RESX example](https://github.com/conceptdev/xamarin-forms-samples/tree/master/TodoL10nResx) of localizing Xamarin.Forms.

![screenshot](https://raw.githubusercontent.com/conceptdev/xamarin-forms-samples/master/TodoL10nVernacular/Screenshots/iOS-Franc%CC%A7ais-sml.png "iOS French") ![screenshot](https://raw.githubusercontent.com/conceptdev/xamarin-forms-samples/master/TodoL10nVernacular/Screenshots/iOS-Deutsch-sml.png "iOS German")

![screenshot](https://raw.githubusercontent.com/conceptdev/xamarin-forms-samples/master/TodoL10nVernacular/Screenshots/Android-Español-sml.png "Android Spanish")  .    ![screenshot](https://raw.githubusercontent.com/conceptdev/xamarin-forms-samples/master/TodoL10nVernacular/Screenshots/Android-Franc%CC%A7ais-sml.png "Android French")


History
------

Many Xamarin code samples are based on the simple [Tasky](https://github.com/xamarin/mobile-samples/tree/master/Tasky) sample, which has been converted to a [Xamarin.Forms](http://xamarin.com/forms) [Todo sample](https://github.com/xamarin/xamarin-forms-samples/tree/master/Todo).

There is also a **Xamarin.Forms** [Xaml version](https://github.com/conceptdev/xamarin-forms-samples/tree/master/TodoXaml) and an [Mvvm version](https://github.com/conceptdev/xamarin-forms-samples/tree/master/TodoMvvm)