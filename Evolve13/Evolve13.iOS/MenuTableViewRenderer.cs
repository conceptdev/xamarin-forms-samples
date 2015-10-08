using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;

[assembly:ExportRenderer(typeof(Evolve13.MenuTableView), typeof(Evolve13.MenuTableViewRenderer))]
namespace Evolve13
{
	public class MenuTableViewRenderer : TableViewRenderer 
	{

		protected override void OnElementChanged (ElementChangedEventArgs<TableView> e)
		{
			base.OnElementChanged (e);
		
			var tableView = Control as UITableView;

			tableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;

			tableView.BackgroundColor = UIColor.FromRGB (0x2C, 0x3E,0x50);
		}
	}
}

