using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.DualScreen;

namespace dstemplate
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        TwoPaneView DualPane
        {
            get { return twoPaneView; }
        }
        public MainPage()
        {
            InitializeComponent();
        }

        ContentView _pane1, _pane2;
        public ContentView Pane1
        {
            get => _pane1;
            set
            {
                _pane1 = value;
                DualPane.Pane1 = _pane1;
                UpdateLayouts();
            }
        }
        public ContentView Pane2
        {
            get => _pane2;
            set
            {
                _pane2 = value;
                DualPane.Pane2 = _pane2;
                UpdateLayouts();
            }
        }

        public bool DeviceIsSpanned => DualScreenInfo.Current.SpanMode != TwoPaneViewMode.SinglePane;

        public void UpdateLayouts()
        {
            if (_pane1 == null) return;

            if (_pane1.GetType() == typeof(SettingsView))
            {
                DualPane.MinTallModeHeight = 0;
                DualPane.MinWideModeWidth = 0;
                DualPane.Pane1Length = new GridLength(2, GridUnitType.Star);
                DualPane.Pane2Length = new GridLength(3, GridUnitType.Star);
                if (DeviceIsSpanned)
                {
                    DualPane.TallModeConfiguration = TwoPaneViewTallModeConfiguration.TopBottom;
                    DualPane.WideModeConfiguration = TwoPaneViewWideModeConfiguration.LeftRight;
                }
                else
                {
                    DualPane.TallModeConfiguration = TwoPaneViewTallModeConfiguration.SinglePane;
                    DualPane.WideModeConfiguration = TwoPaneViewWideModeConfiguration.SinglePane;
                }
            }
            else
            {
                DualPane.MinTallModeHeight = 0;
                DualPane.MinWideModeWidth = 600;
                DualPane.Pane1Length = new GridLength(1, GridUnitType.Star);
                DualPane.Pane2Length = new GridLength(1, GridUnitType.Star);
                DualPane.TallModeConfiguration = TwoPaneViewTallModeConfiguration.TopBottom;
                DualPane.WideModeConfiguration = TwoPaneViewWideModeConfiguration.LeftRight;
            }
        }
    }
}
