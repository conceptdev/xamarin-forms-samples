using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Todo
{
	public class TodoListPage : ContentPage
	{
		ListView listView;
		Image newImage;
		RelativeLayout layout;

		public TodoListPage ()
		{
			Title = "Todo";

			listView = new ListView {StyleId = "TodoList"};
			listView.ItemTemplate = new DataTemplate (typeof (TodoItemCell));

			listView.ItemSelected += async (sender, e) => {
				if (e.SelectedItem == null) {
					return; // ensures we ignore this handler when the selection is just being cleared
				}
				var todoItem = (TodoItem)e.SelectedItem;
				var todoPage = new TodoItemPage();
				todoPage.BindingContext = todoItem;
				await Navigation.PushAsync(todoPage);
				((ListView)sender).SelectedItem = null; // clears the 'selected' background
			};

			// make floating (+) image at bottom of screen
			var tap = new TapGestureRecognizer (async (View obj) => {
				var todoItem = new TodoItem();
				var todoPage = new TodoItemPage();
				todoPage.BindingContext = todoItem;

				var b = newImage.Bounds;
				b.Y = b.Y - 50;

				await newImage.LayoutTo(b,250, Easing.SinIn);
				b.Y = b.Y + 50;
				await newImage.LayoutTo(b,250, Easing.SinOut);

				await Navigation.PushAsync(todoPage);
			});
			newImage = new Image {
				Source = "newitem.png",
				WidthRequest = 40,
				Opacity = 0.8f,
				StyleId = "TodoAdd"
			};
			newImage.GestureRecognizers.Add (tap);


			AccessibilityEffect.SetIsAccessible(newImage, true);
			AccessibilityEffect.SetAccessibilityTraits(newImage, AccessibilityTrait.Button);
			AccessibilityEffect.SetAccessibilityLabel(newImage, "Add new item");


			layout = new RelativeLayout ();
			layout.Children.Add (listView, 
				xConstraint: Constraint.Constant(0), 
				yConstraint: Constraint.Constant(0), 
				widthConstraint: Constraint.RelativeToParent ((parent) => {return parent.Width;}),
				heightConstraint: Constraint.RelativeToParent ((parent) => {return parent.Height;}));
			layout.Children.Add (newImage, 
				xConstraint: Constraint.RelativeToParent((parent) =>
					{
						return (parent.Width / 2) - 20; // center of image (which is 40 wide)
					}),
				yConstraint: Constraint.RelativeToParent((parent) =>
					{
						return parent.Height - 60;
					}));
			Content = layout;


			#region toolbar
			var tbiAdd = new ToolbarItem ("+", "plus.png", () => {
				var todoItem = new TodoItem();
				var todoPage = new TodoItemPage();
				todoPage.BindingContext = todoItem;
				Navigation.PushAsync(todoPage);
			}, 0, 0);
			tbiAdd.StyleId = "ToolbarAdd";
			//tbiAdd.Order = ToolbarItemOrder.Secondary;
			ToolbarItems.Add (tbiAdd);

			AccessibilityEffect.SetIsAccessible(tbiAdd, true);
			AccessibilityEffect.SetAccessibilityLabel(tbiAdd, "Add new item");
			;


			var tbiSpeak = new ToolbarItem ("?", "chat.png", () => {
				var todos = App.Database.GetItemsNotDone();
				var tospeak = "";
				foreach (var t in todos)
					tospeak += t.Name + " ";
				if (tospeak == "") tospeak = "there are no tasks to do";

				DependencyService.Get<ITextToSpeech>().Speak("Hello from Xamarin Forms");

			}, 0, 0);
			tbiSpeak.StyleId = "ToolbarSpeak";
			// demonstrate toolbar/optionmenu
			//tbiSpeak.Order = ToolbarItemOrder.Secondary;
			ToolbarItems.Add (tbiSpeak);

			AccessibilityEffect.SetIsAccessible(tbiSpeak, true);
			AccessibilityEffect.SetAccessibilityLabel(tbiSpeak, "Speak item list");

			#endregion
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			listView.ItemsSource = App.Database.GetItems ();
		}
	}
}
