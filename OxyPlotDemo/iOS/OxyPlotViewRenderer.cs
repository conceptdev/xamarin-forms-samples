using System;
using Xamarin.Forms.Platform.iOS;
using MonoTouch.UIKit;
using OxyPlot;
using OxyPlot.XamarinIOS;
using OxyPlot.Series;
using System.Collections.Generic;
using System.Drawing;
using OxyPlotDemo;
using OxyPlotDemo.iOS;
using Xamarin.Forms;
using System.ComponentModel;


[assembly: ExportRenderer (typeof (OxyPlotView), typeof (OxyPlotViewRenderer))]

namespace OxyPlotDemo.iOS
{
	public class OxyPlotViewRenderer : ViewRenderer<OxyPlotView, PlotView>
	{
		protected override void OnElementChanged (ElementChangedEventArgs<OxyPlotView> e)
		{
			base.OnElementChanged (e);

			SetNativeControl (new PlotView ());

//			var Points = new List<DataPoint>
//			{
//				new DataPoint(0, 4),
//				new DataPoint(10, 13),
//				new DataPoint(20, 15),
//				new DataPoint(30, 16),
//				new DataPoint(40, 12),
//				new DataPoint(50, 12)
//			};
//
//			var m = new PlotModel ("Titleee");
//			m.PlotType = PlotType.XY;
//
//			var s = new LineSeries ();
//			s.ItemsSource = Points;
//			m.Series.Add (s);

			Control.Model = Element.Model;

			Control.BackgroundColor = Element.BackgroundColor.ToUIColor ();
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			if (Control != null)
				Control.Frame = new RectangleF (0, 0, (float) Element.Width, (float) Element.Height);
		}

		protected override void OnElementPropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);
			if (e.PropertyName == BoxView.ColorProperty.PropertyName) {
				Control.BackgroundColor = Element.BackgroundColor.ToUIColor ();
			}
		}

	}
}

