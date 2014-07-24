using OxyPlotDemo.WinPhone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using OxyPlotDemo;
using OxyPlot;
using Xamarin.Forms.Platform.WinPhone;
using OxyPlot.WP8;
using System.Windows.Media;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(OxyPlotView), typeof(OxyPlotViewRenderer))]

namespace OxyPlotDemo.WinPhone
{
    public class OxyPlotViewRenderer : ViewRenderer<OxyPlotView, PlotView>
    {

        protected override void OnElementChanged(ElementChangedEventArgs<OxyPlotView> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || this.Element == null)
                return;
        
            var plotView = new PlotView();

            plotView.Model = Element.Model;
            
            SetNativeControl(plotView);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == OxyPlotView.BackgroundColorProperty.PropertyName)
            {
                var color = System.Windows.Media.Color.FromArgb((byte)Element.BackgroundColor.A, (byte)Element.BackgroundColor.R, (byte)Element.BackgroundColor.G, (byte) Element.BackgroundColor.B);
                Control.Background = new SolidColorBrush(color);
            }
        }
    }
}
