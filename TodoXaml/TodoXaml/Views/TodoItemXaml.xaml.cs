using System;
using Xamarin.Forms;

namespace TodoXaml
{
	public partial class TodoItemXaml : ContentPage
	{
		public TodoItemXaml ()
		{
			InitializeComponent ();	
		}

		void OnSaveActivated (object sender, EventArgs e)
		{
			var todoItem = (TodoItem)BindingContext;
			App.Database.SaveItem(todoItem);
			this.Navigation.PopAsync();
		}

		void OnDeleteActivated (object sender, EventArgs e)
		{
			var todoItem = (TodoItem)BindingContext;
			App.Database.DeleteItem(todoItem.ID);
			this.Navigation.PopAsync();
		}

		void OnCancelActivated (object sender, EventArgs e)
		{
			this.Navigation.PopAsync();
		}

		void OnSpeakActivated (object sender, EventArgs e)
		{
			var todoItem = (TodoItem)BindingContext;
			App.Speech.Speak(todoItem.Name + " " + todoItem.Notes);
		}
	}
}

