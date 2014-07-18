using System;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using Vernacular;

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

			return translated;
		}
	}
}

