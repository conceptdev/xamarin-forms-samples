using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using OxyPlotDemo;
using OxyPlot.Android;
using OxyPlot.XamarinAndroid;
using Android.OS;

[assembly: ExportRenderer (typeof (OxyPlotView), typeof (OxyPlotViewRenderer))]

namespace OxyPlot.Android
{
	public class OxyPlotViewRenderer : ViewRenderer
	{
		protected PlotView NativeControl
		{
			get { return ((PlotView) Control); }
		}
		protected OxyPlotView NativeElement
		{
			get { return ((OxyPlotView) Element); }
		}

		protected override void OnElementChanged (ElementChangedEventArgs<View> e)
		{
			base.OnElementChanged (e);

			var plotView = new PlotView (Context);
			SetNativeControl (plotView);

			NativeControl.Model = NativeElement.Model;

			NativeControl.SetBackgroundColor (NativeElement.BackgroundColor.ToAndroid ());
		}

		protected override void OnLayout (bool changed, int l, int t, int r, int b)
		{
			base.OnLayout (changed, l, t, r, b);
		}

		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);
		
			if (e.PropertyName == BoxView.ColorProperty.PropertyName) {
				NativeControl.SetBackgroundColor (NativeElement.BackgroundColor.ToAndroid ());
			}
		}
	}
}

