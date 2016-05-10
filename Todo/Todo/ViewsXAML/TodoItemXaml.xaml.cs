using System;
using Xamarin.Forms;

namespace Todo
{
	public partial class TodoItemXaml : ContentPage
	{
		public TodoItemXaml ()
		{
			InitializeComponent ();	
		}

		async void OnSaveActivated (object sender, EventArgs e)
		{
			var todoItem = (TodoItem)BindingContext;
			App.Database.SaveItem(todoItem);
			await this.Navigation.PopAsync();
		}

		async void OnDeleteActivated (object sender, EventArgs e)
		{
			var todoItem = (TodoItem)BindingContext;
			App.Database.DeleteItem(todoItem.ID);
			await this.Navigation.PopAsync();
		}

		void OnCancelActivated (object sender, EventArgs e)
		{
			this.Navigation.PopAsync();
		}

		void OnSpeakActivated (object sender, EventArgs e)
		{
			var todoItem = (TodoItem)BindingContext;
			DependencyService.Get<ITextToSpeech>().Speak(todoItem.Name + " " + todoItem.Notes);
		}
	}
}

