using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace TodoXaml
{
	public partial class TodoCellXaml : ContentView
	{
		public TodoCellXaml ()
		{
			InitializeComponent ();
		}
	}

	public class TodoCell : ViewCell
	{
		public TodoCell () {
			View = new TodoCellXaml ();
		}
	}
}

