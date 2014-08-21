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
using Xamarin;


[assembly: ExportRenderer (typeof (OxyPlotView), typeof (OxyPlotViewRenderer))]

namespace OxyPlotDemo.iOS
{
	public class OxyPlotViewRenderer : ViewRenderer<OxyPlotView, PlotView>
	{
		protected override void OnElementChanged (ElementChangedEventArgs<OxyPlotView> e)
		{
			base.OnElementChanged (e);
			if (e.OldElement != null || this.Element == null)
				return;

			var plotView = new PlotView ();

			SetNativeControl (plotView);

			Element.OnInvalidateDisplay += (s,ea) => {
				plotView.SetNeedsDisplay();
				Control.Model.InvalidatePlot(true);
				Control.InvalidatePlot(true);
			};

			Control.Model = Element.Model;

			Control.BackgroundColor = Element.BackgroundColor.ToUIColor ();


		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			if (Control != null)
				Control.Frame = new RectangleF (0, 0, (float) Element.Width, (float) Element.Height);

			Insights.Track ("LAYOUT-iOS");
		}

		protected override void OnElementPropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);
			if (e.PropertyName == OxyPlotView.BackgroundColorProperty.PropertyName) {
				Control.BackgroundColor = Element.BackgroundColor.ToUIColor ();
				Insights.Track ("BACKGROUND-iOS");
			}
			if (e.PropertyName == OxyPlotView.ModelProperty.PropertyName) {
				Control.Model.InvalidatePlot(true);
				Control.InvalidatePlot(true);
			}
		}

	}
}

