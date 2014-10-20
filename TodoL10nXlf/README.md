TodoL10nXlf (Xamarin.Forms)
=============

This sample uses [Multilingual App Toolkit (MAT) from Microsoft](https://dev.windows.com/en-us/develop/multilingual-app-toolkit) to localize a Xamarin.Forms application for iOS, Android, and Windows Phone. You can learn about MAT here:

* [Introduction to MAT video](https://channel9.msdn.com/Series/Introducing-Windows-8/Introduction-to-the-Multilingual-App-Toolkit)

* [MAT and Xamarin blog post](http://blogs.msdn.com/b/matdev/archive/2014/10/08/mat-v4-0-technical-preview-adds-xamarin-support.aspx)

**MAT** stores language information in [XLIFF](https://www.oasis-open.org/committees/tc_home.php?wg_abbrev=xliff) (.xlf) files which are parsed into RESX files at build time. It is the RESX files that are loaded by the application to render the translated user-interface. The XLIFF files are **edited in Visual Studio** and the build step that transforms them only runs there, so language data should only be edited on Windows... luckily this runs in Visual Studio Express. You can then push your app (including the generated RESX files) into source control - they'll work fine for iOS, Android, and Windows Phone projects.

**SEE ALSO** the [RESX example](https://github.com/conceptdev/xamarin-forms-samples/tree/master/TodoL10nResx) of localizing Xamarin.Forms.

![screenshot](https://raw.githubusercontent.com/conceptdev/xamarin-forms-samples/master/TodoL10nVernacular/Screenshots/iOS-Franc%CC%A7ais-sml.png "iOS French") ![screenshot](https://raw.githubusercontent.com/conceptdev/xamarin-forms-samples/master/TodoL10nVernacular/Screenshots/iOS-Deutsch-sml.png "iOS German")

![screenshot](https://raw.githubusercontent.com/conceptdev/xamarin-forms-samples/master/TodoL10nVernacular/Screenshots/Android-Español-sml.png "Android Spanish")  .    ![screenshot](https://raw.githubusercontent.com/conceptdev/xamarin-forms-samples/master/TodoL10nVernacular/Screenshots/Android-Franc%CC%A7ais-sml.png "Android French")


History
------

Many Xamarin code samples are based on the simple [Tasky](https://github.com/xamarin/mobile-samples/tree/master/Tasky) sample, which has been converted to a [Xamarin.Forms](http://xamarin.com/forms) [Todo sample](https://github.com/xamarin/xamarin-forms-samples/tree/master/Todo).

There is also a **Xamarin.Forms** [Xaml version](https://github.com/conceptdev/xamarin-forms-samples/tree/master/TodoXaml) and an [Mvvm version](https://github.com/conceptdev/xamarin-forms-samples/tree/master/TodoMvvm)