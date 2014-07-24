using System;
using Xamarin.Forms;
using System.Collections.Generic;
using ExampleLibrary;

namespace OxyPlotDemo
{
	public class ExampleList : ContentPage
	{
		List<ExampleInfo> exampleInfoList;

		public ExampleList ()
		{
			// TODO: maybe i'll, you know, sort and group this a bit...
			exampleInfoList = ExampleLibrary.Examples.GetList();

			var listView = new ListView ();
			listView.ItemTemplate = new DataTemplate (typeof(TextCell));
			listView.ItemsSource = exampleInfoList;
			listView.ItemTemplate.SetBinding (TextCell.TextProperty, "Title");
			listView.ItemSelected += (sender, e) => {
				var ei = (ExampleInfo)e.SelectedItem;
				var g = new GraphViewPage(ei);
				g.Title = ei.Title;
				Navigation.PushAsync(g);
			};
			Content = new StackLayout {
				Children = {listView}
			};
		}
	}
}

