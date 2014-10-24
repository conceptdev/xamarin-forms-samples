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
		public TodoListPage ()
		{
			Title = "Todo";

			NavigationPage.SetHasNavigationBar (this, true);

			listView = new ListView {
			    RowHeight = 40
			};
			listView.ItemTemplate = new DataTemplate (typeof (TodoItemCell));

			listView.ItemSelected += (sender, e) => {
				var todoItem = (TodoItem)e.SelectedItem;
				var todoPage = new TodoItemPage();
				todoPage.BindingContext = todoItem;
				Navigation.PushAsync(todoPage);
			};

			var layout = new StackLayout();
			if (Device.OS == TargetPlatform.WinPhone) { // WinPhone doesn't have the title showing
				layout.Children.Add(new Label{Text="Todo", Font=Font.SystemFontOfSize(NamedSize.Large, FontAttributes.Bold)});
			}
			layout.Children.Add(listView);
			layout.VerticalOptions = LayoutOptions.FillAndExpand;
			Content = layout;


			var tbiAdd = new ToolbarItem ("+", "plus.png", () => {
				var todoItem = new TodoItem();
				var todoPage = new TodoItemPage();
				todoPage.BindingContext = todoItem;
				Navigation.PushAsync(todoPage);
			}, 0, 0);

			ToolbarItems.Add (tbiAdd);

			var tbiSpeak = new ToolbarItem ("?", "chat.png", () => {
				var todos = App.Database.GetItemsNotDone();
				var tospeak = "";
				foreach (var t in todos)
					tospeak += t.Name + " ";
				if (tospeak == "") tospeak = "there are no tasks to do";

				DependencyService.Get<ITextToSpeech>().Speak(tospeak);
			}, 0, 0);
			ToolbarItems.Add (tbiSpeak);

		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			listView.ItemsSource = App.Database.GetItems ();
		}
	}
}

