# Surface Duo + Xamarin step 3

**Microsoft Build 2020**

This code sample was used in the Microsoft Build 2020 session [_How to bring your Android apps to Surface Duo with Xamarin_](http://aka.ms/M365sk123).

In this step the sample is modified to support dual-screen display using the `TwoPaneView`. These files are added/changed:

- Notes.Android/MainActivity.cs - `DualScreenService.Init(this);`
- Notes/App.xaml.cs - use new `MainPage`
- Notes/NoteEntryPage.xaml+cs - convert to ContentView, move nav/lifecycle methods
- Notes/NotesPage.xaml+cs - convert to ContentView, move nav/lifecycle methods
- Notes/MainPage.xaml+cs - hosts `TwoPaneView` and nav/lifecycle methods

Learn more about the [`TwoPaneView`](https://docs.microsoft.com/dual-screen/xamarin/twopaneview) in the docs.
