using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;

namespace Todo
{
	public class TodoItemPage : ContentPage
	{
		Image doneImage, cancelImage;

		public TodoItemPage ()
		{
			this.SetBinding (ContentPage.TitleProperty, "Name");
			BackgroundColor = Color.FromRgb (255, 244, 165);
			NavigationPage.SetHasNavigationBar (this, true);

			var nameLabel = new Label { Text = "Name", Font = Constants.Font };
			var nameEntry = new Entry { Placeholder = "what to do?" };
			
			nameEntry.SetBinding (Entry.TextProperty, "Name");

			var notesLabel = new Label { Text = "Notes", Font = Constants.Font  };
			var notesEntry = new Entry { Placeholder = "more info..." };
			notesEntry.SetBinding (Entry.TextProperty, "Notes");

			var doneLabel = new Label { Text = "Done", Font = Constants.Font, HorizontalOptions = LayoutOptions.StartAndExpand };

			doneImage =new Image {
				Source = ImageSource.FromFile("box.png"),
				HorizontalOptions = LayoutOptions.EndAndExpand,
			}; 
			var doneTap = new TapGestureRecognizer {
				NumberOfTapsRequired = 1
			};
			doneTap.Tapped += (object sender, EventArgs e) => {
				var todo = (TodoItem)BindingContext;
				todo.Done = !todo.Done;
				if (todo.Done)
					doneImage.Source = ImageSource.FromFile("checkbox.png"); 
				else
					doneImage.Source = ImageSource.FromFile("box.png"); 
			};
			doneImage.GestureRecognizers.Add (doneTap);


			var saveImage = new Image {Source = ImageSource.FromFile("save.png"), HorizontalOptions = LayoutOptions.CenterAndExpand};
			cancelImage = new Image {Source = ImageSource.FromFile("cancel.png"), HorizontalOptions = LayoutOptions.CenterAndExpand};
			var speakImage = new Image {Source = ImageSource.FromFile("speak.png"), HorizontalOptions = LayoutOptions.CenterAndExpand};

			var saveTap = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
			var cancelTap = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
			var speakTap = new TapGestureRecognizer { NumberOfTapsRequired = 1 };

			saveTap.Tapped += async (sender, e) => {
				await saveImage.FadeTo(0.3);
				var todoItem = (TodoItem)BindingContext;
				App.Database.SaveItem(todoItem);
				await Navigation.PopAsync();
			};
            cancelTap.Tapped += async (sender, e) =>
            {
                var todoItem = (TodoItem)BindingContext;
                if (todoItem.ID <= 0)
                {
                    await Navigation.PopAsync();
                }  else {
                    await cancelImage.FadeTo(0.3);
                    var doit = await DisplayAlert("Are you sure?", "Do you want to delete the task " + todoItem.Name + "?", "Delete", "Cancel");
                    if (doit)
                    {
                        App.Database.DeleteItem(todoItem.ID);
                        await Navigation.PopAsync();
                    }
                    await cancelImage.FadeTo(1.0);
                }
            };
			speakTap.Tapped += async (sender, e) => {
				await speakImage.FadeTo(0.3);
				var todoItem = (TodoItem)BindingContext;
				App.Speech.Speak(todoItem.Name + " " + todoItem.Notes);
				await speakImage.FadeTo(1.0);
			};
			saveImage.GestureRecognizers.Add (saveTap);
			cancelImage.GestureRecognizers.Add (cancelTap);
			speakImage.GestureRecognizers.Add (speakTap);


			if (Device.OS == TargetPlatform.iOS) {
				nameEntry.BackgroundColor = Color.FromRgb (255, 244, 165);
				notesEntry.BackgroundColor = Color.FromRgb (255, 244, 165);
			}
			if (Device.OS != TargetPlatform.iOS) { // WinPhone & Android
				nameEntry.TextColor = Color.Black;
				notesEntry.TextColor = Color.Black;

				nameLabel.TextColor = Color.Black;
				notesLabel.TextColor = Color.Black;
				doneLabel.TextColor = Color.Black;
			}

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness(20),
				Children = {
					nameLabel, nameEntry, 
					notesLabel, notesEntry,
					new StackLayout {
						Orientation = StackOrientation.Horizontal,
						HorizontalOptions = LayoutOptions.StartAndExpand,
						VerticalOptions = LayoutOptions.Center,
						Children = {
							doneLabel,
							doneImage
						}
					},
					new StackLayout {
						Orientation = StackOrientation.Horizontal,
						HorizontalOptions = LayoutOptions.CenterAndExpand,
						Children = {
							saveImage, //deleteImage, 
                            cancelImage, speakImage
							//saveButton, deleteButton, cancelButton, speakButton
						}
					}

				}
			};
		}
		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();

			var todo = (TodoItem)BindingContext;
			var initialDoneImage = "box.png";
			if (todo.Done) 
				initialDoneImage = "checkbox.png";
			doneImage.Source = ImageSource.FromFile(initialDoneImage);

			if (todo.ID <= 0) {
				//deleteImage.IsVisible = false;
                cancelImage.Source = ImageSource.FromFile("cancel.png");
			} else {
				//deleteImage.IsVisible = true;
				cancelImage.Source = ImageSource.FromFile("delete1.png");
			}
		}
	}
}