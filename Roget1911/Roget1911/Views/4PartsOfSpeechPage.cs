using System;
using Xamarin.Forms;
using System.Text;
using System.Collections.Generic;

namespace Roget1911
{
	public class PartsOfSpeechPage : ContentPage
	{
		WebView web;
		public PartsOfSpeechPage (RogetCategory cat)
		{
			NavigationPage.SetHasNavigationBar (this, true);

			web = new WebView {
				WidthRequest = 400,
				HeightRequest = 800
			}; // want this to just always go full-screen...

			var htmlSource = new HtmlWebViewSource ();
			htmlSource.Html = FormatText(cat.PartsOfSpeech);
			web.Source = htmlSource;

			// test PopToRootAsync bugfix in 1.2.2-pre2
			var b = new Button { Text = "Main Menu" };
			b.Clicked += (sender, e) => {
				Navigation.PopToRootAsync();
			};

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.StartAndExpand,
				Children = {b, web}
			};

		}

		/// <summary>
		/// Format the parts-of-speech text for UIWebView
		/// </summary>
		private string FormatText(List<RogetPartOfSpeech> partsOfSpeech)
		{
			StringBuilder sb = new StringBuilder();

			foreach (var part in partsOfSpeech)
			{
				sb.Append("<style>body,b,p{font-family:Helvetica;}</style>");
				sb.Append("<b>"+part.PartOfSpeech+"</b><br/>"+ Environment.NewLine);
				foreach (var line in part.Lines)
				{
					sb.Append(line + "<br/>" + Environment.NewLine);
				}
				sb.Append("<br/>");
			}
			return sb.ToString();
		}
	}
}

