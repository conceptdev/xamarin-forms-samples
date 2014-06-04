using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Social.Services;
using Xamarin.Social;

[assembly:ExportRenderer(typeof(Evolve13.TweetButton), typeof(Evolve13.TweetButtonRenderer))]

namespace Evolve13
{
	public class TweetButtonRenderer : ButtonRenderer
	{
		protected override void OnModelChanged (VisualElement oldModel, VisualElement newModel)
		{
			base.OnModelChanged (oldModel, newModel);

			var tweetButton = newModel as TweetButton;

			var button = Control as global::Android.Widget.Button;

			button.Click += (sender, e) => {
				var message = "";
				try {
					var intent = new Intent(Intent.ActionSend);
					intent.PutExtra(Intent.ExtraText,message);
					intent.SetType("text/plain");
					Forms.Context.StartActivity(Intent.CreateChooser(intent, tweetButton.Tweet));

				} catch(Exception ex) {
					System.Diagnostics.Debug.WriteLine (ex);
				}
			};
		}
	}
}
