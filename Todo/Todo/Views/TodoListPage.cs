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

			NavigationPage.SetHasNavigationBar (this, true);

			listView = new ListView ();
			listView.ItemTemplate = new DataTemplate (typeof (TodoItemCell));

//			listView.ItemSource = new string [] { "Buy pears", "Buy oranges", "Buy mangos", "Buy apples", "Buy bananas" };
//			listView.ItemSource = new TodoItem [] { 
//				new TodoItem {Name = "Buy pears`"}, 
//				new TodoItem {Name = "Buy oranges`", Done=true},
//				new TodoItem {Name = "Buy mangos`"}, 
//				new TodoItem {Name = "Buy apples`", Done=true},
//				new TodoItem {Name = "Buy bananas`", Done=true}
//			};

			listView.ItemSelected += (sender, e) => {
				if (e.SelectedItem == null) {
					return; // ensures we ignore this handler when the selection is just being cleared
				}
				var todoItem = (TodoItem)e.SelectedItem;
				var todoPage = new TodoItemPage();
				todoPage.BindingContext = todoItem;
				Navigation.PushAsync(todoPage);
				((ListView)sender).SelectedItem = null; // clears the 'selected' background
			};

			// make floating (+) image at bottom of screen
			var tap = new TapGestureRecognizer ((View obj) => {
				var todoItem = new TodoItem();
				var todoPage = new TodoItemPage();
				todoPage.BindingContext = todoItem;
				Navigation.PushAsync(todoPage);
			});
			newImage = new Image {
				Source = "newitem.png",
				WidthRequest = 40
			};
			newImage.GestureRecognizers.Add (tap);

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
			var tbi = new ToolbarItem ("+", "plus.png", () => {
				var todoItem = new TodoItem();
				var todoPage = new TodoItemPage();
				todoPage.BindingContext = todoItem;
				Navigation.PushAsync(todoPage);
			}, 0, 0);

			ToolbarItems.Add (tbi);

			if (Device.OS == TargetPlatform.iOS) {
				var tbi2 = new ToolbarItem ("?", "chat.png", () => {
					var todos = App.Database.GetItemsNotDone();
					var tospeak = "";
					foreach (var t in todos)
						tospeak += t.Name + " ";
					if (tospeak == "") tospeak = "there are no tasks to do";
					App.Speech.Speak(tospeak);
				}, 0, 0);
				ToolbarItems.Add (tbi2);
			}
			#endregion
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			listView.ItemsSource = App.Database.GetItems ();
		}
	}
}

