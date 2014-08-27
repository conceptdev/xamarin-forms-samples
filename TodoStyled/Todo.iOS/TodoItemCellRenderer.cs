//using System;
//using Todo;
//using Xamarin.Forms;
//using Todo.iOS;
//
///* Example of using a custom renderer to get the > disclosure indicator to appear */
//
//[assembly: ExportRenderer (typeof (TodoItemCell), typeof (TodoItemCellRenderer))]
//
//namespace Todo.iOS
//{
//	public class TodoItemCellRenderer : Xamarin.Forms.Platform.iOS.ViewCellRenderer
//	{
//		public override MonoTouch.UIKit.UITableViewCell GetCell (Cell item, MonoTouch.UIKit.UITableView tv)
//		{
//			var cell = base.GetCell (item, tv);
//
//			cell.Accessory = MonoTouch.UIKit.UITableViewCellAccessory.DisclosureIndicator;
//
//			return cell;
//		}
//	}
//}