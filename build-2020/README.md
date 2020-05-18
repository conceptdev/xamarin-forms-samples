# Surface Duo + Xamarin at Microsoft Build 2020

These code samples were used in the Microsoft Build 2020 session [_How to bring your Android apps to Surface Duo with Xamarin_](http://aka.ms/M365sk123).

## [Step 1](step-1/)

The session begins with the code for this [quickstart documentation](https://docs.microsoft.com/xamarin/get-started/quickstarts/multi-page). It's been copied from the [Xamarin samples GitHub](https://github.com/xamarin/xamarin-forms-samples/tree/master/GetStarted/Notes/MultiPage) to this folder for reference.

In the session, this sample is used to test how the app works on the [Surface Duo emulator](https://docs.microsoft.com/dual-screen/android/get-duo-sdk).

## [Step 2](step-2/)

In this step the sample is modified to support Android drag-and-drop. Three files are added/changed:

- **Notes/ItemDragAndDropEffect.cs** - Xamarin.Forms effect in common code
- **Notes.Android/ItemDragAndDropEffect.cs** - Effect implementation for Android
- **NotesPage.xaml** - Effect added to XAML

The [DragAndDrop sample](https://github.com/microsoft/surface-duo-sdk-xamarin-samples/tree/master/DragAndDrop) can be used as the 'drag target' to test.

## [Step 3](step-3/)

In this step the sample is modified to support dual-screen display using the `TwoPaneView`. These files are added/changed:

- Notes.Android/MainActivity.cs - `DualScreenService.Init(this);`
- Notes/App.xaml.cs - use new `MainPage`
- Notes/NoteEntryPage.xaml+cs - convert to ContentView, move nav/lifecycle methods
- Notes/NotesPage.xaml+cs - convert to ContentView, move nav/lifecycle methods
- Notes/MainPage.xaml+cs - hosts `TwoPaneView` and nav/lifecycle methods

Learn more about the [`TwoPaneView`](https://docs.microsoft.com/dual-screen/xamarin/twopaneview) in the docs.

## Next steps

Other samples:

- [Duo Notes](https://github.com/conceptdev/xamarin-forms-samples/tree/master/duo-notes) - further enhanced version of this Notes demo.
- [XamarinTV](https://github.com/xamarin/app-xamarintv)
- [Food delivery](https://github.com/jsuarezruiz/FoodDeliveryAppDuo)
