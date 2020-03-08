using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace dstemplate
{
    public partial class SettingsView : ContentView, INotifyPropertyChanged
    {
        public SettingsView()
        {
            InitializeComponent();

            BindingContext = this;

            IsDarkMode = (App.AppTheme == "dark");
        }
        bool _isDarkMode;
        public bool IsDarkMode
        {
            get => _isDarkMode;
            set {
                _isDarkMode = value;
                OnPropertyChanged("IsDarkMode");
            }
        }

        void Close_Clicked(System.Object sender, System.EventArgs e)
        {
            App._mainPage.Pane1 = new ListingView();
        }
        void GitHub_Clicked(System.Object sender, System.EventArgs e)
        {
            string url = "https://github.com/microsoft/surface-duo-sdk-xamarin-samples";
            Browser.OpenAsync(new Uri(url));
        }

        void ThemeLight_Tapped(System.Object sender, System.EventArgs e)
        {
            IsDarkMode = false;
            Application.Current.Resources = new LightTheme();
            App.AppTheme = "light";
        }

        void ThemeDark_Tapped(System.Object sender, System.EventArgs e)
        {
            IsDarkMode = true;
            Application.Current.Resources = new DarkTheme();
            App.AppTheme = "dark";
        }

        
    }
}
