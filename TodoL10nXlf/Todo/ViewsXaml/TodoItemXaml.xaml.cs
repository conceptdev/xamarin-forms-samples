using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Todo
{	
	public partial class TodoItemXaml : ContentPage
	{	
		public TodoItemXaml ()
		{
			InitializeComponent ();
		}

		void OnSave (object s, EventArgs e) {}
		void OnDelete (object s, EventArgs e) {}
		void OnCancel (object s, EventArgs e) {}
		void OnSpeak (object s, EventArgs e) {}
	}
}

