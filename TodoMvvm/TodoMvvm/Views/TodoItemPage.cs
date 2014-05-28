using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TodoMvvm
{
	public class TodoItemPage : ContentPage
	{
		string name, description;

		public TodoItemPage ()
		{
			this.SetBinding (ContentPage.TitleProperty, "Name");

			NavigationPage.SetHasNavigationBar (this, true);
			var nameLabel = new Label { Text = "Name" };
			var nameEntry = new Entry { Text = "<new>" };
			nameEntry.SetBinding (Entry.TextProperty, "Name");

			var notesLabel = new Label { Text = "Notes" };
			var notesEntry = new Entry ();
			notesEntry.SetBinding (Entry.TextProperty, "Notes");

			var doneLabel = new Label { Text = "Done" };
			var doneEntry = new Switch ();
			doneEntry.SetBinding (Switch.IsToggledProperty, "Done");


			var saveButton = new Button { Text = "Save" };
			saveButton.SetBinding (Button.CommandProperty, "SaveCommand");
			var cancelButton = new Button { Text = "Cancel" };
			cancelButton.SetBinding (Button.CommandProperty, "CancelCommand");
			cancelButton.SetBinding (Button.IsVisibleProperty, "CanCancel");
			var deleteButton = new Button { Text = "Delete" };
			deleteButton.SetBinding (Button.CommandProperty, "DeleteCommand");
			deleteButton.SetBinding (Button.IsVisibleProperty, "CanDelete");


			var speakButton = new Button { Text = "Speak" };
			speakButton.SetBinding (Button.CommandProperty, "SpeakCommand");
			speakButton.SetBinding (Button.IsVisibleProperty, "CanSpeak");
		
			Content = new StackLayout {
				VerticalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness(20),
				Children = {nameLabel, nameEntry, 
					notesLabel, notesEntry,
					doneLabel, doneEntry,
					saveButton, cancelButton, deleteButton, speakButton}
			};

		}
	}
}

