using System;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using Vernacular;
using System.Diagnostics;

namespace QuickTodo
{
	[ContentProperty ("Text")]
	public class TranslateExtension : IMarkupExtension
	{
		public string Text { get; set; }
	
		public object ProvideValue (IServiceProvider serviceProvider)
		{
			if (Text == null)
				return null;

			var translated = Catalog.GetString (Text); 


			Debug.WriteLine ("cat:" + Catalog.GetString ("NewTaskPlaceholder"));
			Debug.WriteLine ("cat:" + Catalog.GetString ("DeleteButton"));
			Debug.WriteLine ("cat:" + Catalog.GetString ("Save"));
			Debug.WriteLine ("cat:" + Catalog.GetString ("Delete"));


			return translated;
		}
	}
}

