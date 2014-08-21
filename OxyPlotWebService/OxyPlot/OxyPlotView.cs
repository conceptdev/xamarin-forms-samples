using System;
using Xamarin.Forms;

namespace OxyPlotDemo
{
	public class OxyPlotView : View
	{
		public OxyPlotView ()
		{
			WidthRequest = 40;
			HeightRequest = 40;
		}

		public EventHandler OnInvalidateDisplay;

		public static readonly BindableProperty BackgroundColorProperty = 
			BindableProperty.Create ("BackgroundColor", typeof(Color), typeof(OxyPlotView), Color.Default);

		public Color BackgroundColor {
			get { return (Color)GetValue (BackgroundColorProperty); }
			set { SetValue (BackgroundColorProperty, value); } 
		}

		public static readonly BindableProperty ModelProperty = 
			BindableProperty.Create ("Model", typeof(OxyPlot.PlotModel), typeof(OxyPlotView), null);

		public OxyPlot.PlotModel Model {
			get { return (OxyPlot.PlotModel)GetValue (ModelProperty); }
			set { SetValue (ModelProperty, value); } 
		}

		public void InvalidateDisplay () {
			if (OnInvalidateDisplay != null)
				OnInvalidateDisplay (this, null);
		}
	}
}

