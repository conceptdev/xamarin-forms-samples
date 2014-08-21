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

			var plotView = new PlotView ();

			SetNativeControl (plotView);

			Element.OnInvalidateDisplay += (s,ea) => {
				Control.Model.InvalidatePlot(true);
				Control.InvalidatePlot(true);
				plotView.SetNeedsDisplay();
			};

			Control.Model = Element.Model;
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
			if (e.PropertyName == OxyPlotView.ModelProperty.PropertyName) {
				Control.Model.InvalidatePlot(true);
				Control.InvalidatePlot(true);
			}
		}

	}
}

