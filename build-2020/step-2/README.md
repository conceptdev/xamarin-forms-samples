# Surface Duo + Xamarin step 2

**Microsoft Build 2020**

This code sample is used in the Microsoft Build 2020 session [_Enhancing apps for Surface Duo with Xamarin_](http://aka.ms/M365sk123).

In this step the sample is modified to support Android drag-and-drop. Three files are added/changed:

- **Notes/ItemDragAndDropEffect.cs** - Xamarin.Forms effect in common code
- **Notes.Android/ItemDragAndDropEffect.cs** - Effect implementation for Android
- **NotesPage.xaml** - Effect added to XAML

The [DragAndDrop sample](https://github.com/microsoft/surface-duo-sdk-xamarin-samples/tree/master/DragAndDrop) can be used as the 'drag target' to test.

Once the sample has been shown to work, go to [step 3](../step-3/) to add the **Xamarin.Forms.DualScreen** NuGet and implement `TwoPaneView`.
