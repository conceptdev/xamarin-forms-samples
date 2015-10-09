using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using Todo;
using Todo.iOS;
using UIKit;
using Foundation;

[assembly: ExportRenderer (typeof (BasePage), typeof (BasePageRenderer))]

namespace Todo.iOS
{
	public class BasePageRenderer : PageRenderer, IUIViewControllerPreviewingDelegate
	{
		public BasePageRenderer ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Console.WriteLine ("BasePageRenderer.ViewDidLoad");
		}

		/// <summary>
		/// HACK: just a bit of fun
		/// </summary>
		public override void TouchesMoved (NSSet touches, UIEvent evt)
		{
			base.TouchesMoved (touches, evt);
			UITouch touch = touches.AnyObject as UITouch;
			if (touch != null)
			{
				var force = touch.Force;
				var maxForce = touch.MaximumPossibleForce;
				var alpha = force / maxForce;
				alpha = (nfloat)0.5 + (alpha / 2);
				View.BackgroundColor = UIColor.FromHSBA (1, 1, 1, alpha);
//				View.BackgroundColor = UIColor.Red.ColorWithAlpha (alpha);
			}
		}

		public UIViewController GetViewControllerForPreview (IUIViewControllerPreviewing previewingContext, CoreGraphics.CGPoint location)
		{
			Console.WriteLine ("BasePageRenderer.ViewDidLoad");
			return new UIViewController ();
		}

		public void CommitViewController (IUIViewControllerPreviewing previewingContext, UIViewController viewControllerToCommit)
		{
			Console.WriteLine ("CommitViewController");
		}
	}
}

