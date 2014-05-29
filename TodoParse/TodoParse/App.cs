using System;
using Xamarin.Forms;

namespace TodoXaml
{
	public static class App
	{
		public static Page GetMainPage ()
		{
			var tdlx = new TodoListXaml ();
			var mainNav = new NavigationPage (tdlx);

			return mainNav;
		}

		#region Parse stuff
		static TodoItemManager todoItemManager;

		public static TodoItemManager TodoManager {
			get { return todoItemManager; }
			set { todoItemManager = value; }
		}

		public static void SetTodoItemManager (TodoItemManager todoItemManager)
		{
			TodoManager = todoItemManager;
		}
		#endregion

		#region Text to Speech stuff
		static ITextToSpeech TextToSpeech;
		public static void SetTextToSpeech (ITextToSpeech speech)
		{
			TextToSpeech = speech;
		}
		public static ITextToSpeech Speech {
			get { return TextToSpeech; }
		}
		#endregion
	}
}

