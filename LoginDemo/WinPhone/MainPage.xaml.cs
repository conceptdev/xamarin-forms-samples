using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using Xamarin.Forms;


namespace LoginPattern.WinPhone
{
	public partial class MainPage : Xamarin.Forms.Platform.WinPhone.FormsApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
			SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;

            Forms.Init();
         
			LoadApplication(new LoginPattern.App());
        }
    }
}
