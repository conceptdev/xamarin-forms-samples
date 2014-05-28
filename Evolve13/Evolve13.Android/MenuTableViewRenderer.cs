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

[assembly:ExportRenderer(typeof(Evolve13.MenuTableView), typeof(Evolve13.MenuTableViewRenderer))]
namespace Evolve13
{
	public class MenuTableViewRenderer : TableViewRenderer 
	{
		protected override void OnModelChanged (VisualElement oldModel, VisualElement newModel)
		{
			base.OnModelChanged (oldModel, newModel);

			var tableView = Control as global::Android.Widget.ListView;
			tableView.DividerHeight = 0;
			tableView.SetBackgroundColor (new global::Android.Graphics.Color(0x2C, 0x3E, 0x50));
		}
	}
}

