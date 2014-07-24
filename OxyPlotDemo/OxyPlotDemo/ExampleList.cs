using System;
using Xamarin.Forms;
using System.Collections.Generic;
using ExampleLibrary;
using System.Linq;

namespace OxyPlotDemo
{
	public class ExampleList : ContentPage
	{
		List<ExampleInfo> exampleInfoList;

		public ExampleList ()
		{
			exampleInfoList = ExampleLibrary.Examples.GetList();

			var tableView = new TableView ();
			var root = new TableRoot ("OxyPlot Example Browser");
			var section = new TableSection ();
			section.Add (exampleInfoList
				.GroupBy (e => e.Category)
				.OrderBy (g => g.Key)
				.Select (g => GetCategoryCell (g.Key)));
			root.Add (section);
			tableView.Root = root;

			Content = new StackLayout {
				Children = {tableView}
			};
		}

		Cell GetCategoryCell (string cat) {
			var cell = new TextCell {Text = cat};
			cell.Tapped += (sender, ea) => {;
				var category = cat;

				var tableView = new TableView ();
				var root = new TableRoot (category);
				var section = new TableSection ();
				section.Add (exampleInfoList
					.Where (e => e.Category == category)
					.OrderBy (e => e.Title)
					.Select (e => GetGraphCell(e.Title) ));
				root.Add (section);
				tableView.Root = root;

				var contentPage = new ContentPage {
					Title = category,
					Content = new StackLayout {
						Children = {tableView}
					}
				};

				Navigation.PushAsync (contentPage);
			};
			return cell;
		}

		Cell GetGraphCell (string title)
		{
			var cell = new TextCell { Text = title };
			cell.Tapped += (sender, ea) => {
				var ei = exampleInfoList
					.Where (e => e.Title == title)
					.FirstOrDefault ();

				var gp = new GraphViewPage (ei){Title = title};

				Navigation.PushAsync (gp);
			};
			return cell;
		}
	}
}

