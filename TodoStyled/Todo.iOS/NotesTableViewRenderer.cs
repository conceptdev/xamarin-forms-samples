using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using Todo;
using Todo.iOS;
using UIKit;

[assembly:ExportRenderer(typeof(NotesListView), typeof(NotesListViewRenderer))]


namespace Todo.iOS
{

	public class NotesListViewRenderer : ListViewRenderer 
	{
		protected override void OnElementChanged (ElementChangedEventArgs<ListView> e)
		{
			base.OnElementChanged (e);

			var tableView = Control as UITableView;

			tableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;

			//tableView.BackgroundColor = UIColor.FromRGB (0x2C, 0x3E,0x50);
		}
	}
}
