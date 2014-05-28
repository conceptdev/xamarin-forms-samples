using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Linq;

namespace TodoMvvm
{
	class TodoListViewModel : BaseViewModel
	{
		ObservableCollection<TodoItemCellViewModel> contents = new ObservableCollection<TodoItemCellViewModel> ();
		public ObservableCollection<TodoItemCellViewModel> Contents { 
			get { return contents; } 
			set
			{
				if (contents == value)
					return;
				contents = value;
				OnPropertyChanged ();
			}
		}

		public TodoListViewModel ()
		{
			// HACK: don't need these any more, I packaged a pre-populated SQLite database file
//			App.Database.SaveItem (new TodoItem{ Name = "Buy apples", Notes = "Macintosh" });
//			App.Database.SaveItem (new TodoItem{ Name = "Buy pears", Notes = "" });
//			App.Database.SaveItem (new TodoItem{ Name = "Buy milk", Notes = "Soy" });
//			App.Database.SaveItem (new TodoItem{ Name = "Buy bananas", Notes = "" });
//			App.Database.SaveItem (new TodoItem{ Name = "Buy kiwi", Notes = "" });
//			App.Database.SaveItem (new TodoItem{ Name = "Buy oranges", Notes = "" });

			var all = App.Database.GetItems ().ToList();

			foreach (var t in all) {
				contents.Add (new TodoItemCellViewModel (t));
			}

			MessagingCenter.Subscribe<TodoItemViewModel, TodoItem> (this, "TodoSaved", (sender, model) => {
				App.Database.SaveItem(model);
				Reload();
			});

			MessagingCenter.Subscribe<TodoItemViewModel, TodoItem> (this, "TodoDeleted", (sender, model) => {
				App.Database.DeleteItem(model.ID);
				Reload();
			});

			MessagingCenter.Subscribe<TodoItemViewModel, TodoItem> (this, "TodoSpeak", (sender, model) => {
				App.Speech.Speak(model.Name + " " + model.Notes);
			});

			MessagingCenter.Subscribe<TodoListPage, TodoItem> (this, "TodoAdd", (sender, viewModel) => {
				var todo = new TodoItem();
				var todovm = new TodoItemViewModel(todo);
				Navigation.Push (ViewFactory.CreatePage (todovm));
			});

			MessagingCenter.Subscribe<TodoListPage> (this, "TodoListSpeak", (sender) => {
				var todos = App.Database.GetItemsNotDone();
				var tospeak = "";
				foreach (var t in todos)
					tospeak += t.Name + " ";
				if (tospeak == "") tospeak = "there are no tasks to do";
				App.Speech.Speak(tospeak);
			});
		}

		void Reload() 
		{
			var all = App.Database.GetItems ().ToList();

			// HACK: this kinda breaks iOS "NSInternalInconsistencyException". Works fine in Android.
//			Contents.Clear ();
//			foreach (var t in all) {
//				Contents.Add (new TodoItemCellViewModel (t));
//			}

			// HACK: this works in iOS.
			var x = new ObservableCollection<TodoItemCellViewModel> ();
			foreach (var t in all) {
				x.Add (new TodoItemCellViewModel (t));
			}
			Contents = x;
		}

		object selectedItem;
		public object SelectedItem
		{
			get { return selectedItem; }
			set
			{
				if (selectedItem == value)
					return;
				// something was selected
				selectedItem = value;

				OnPropertyChanged ();

				if (selectedItem != null) {

					var todovm = new TodoItemViewModel (((TodoItemCellViewModel)selectedItem).Item);

					Navigation.Push (ViewFactory.CreatePage (todovm));

					selectedItem = null;
				}
			}
		}
	}
}

