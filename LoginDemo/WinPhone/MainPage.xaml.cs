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
    public partial class MainPage : PhoneApplicationPage, ILoginManager
    {
        public MainPage()
        {
            InitializeComponent();

            Forms.Init();
            Content = LoginPattern.App.GetLoginPage(this).ConvertPageToUIElement(this);
        }

        public void ShowMainPage()
        {
            Content = LoginPattern.App.GetMainPage().ConvertPageToUIElement(this);
        }

        public void Logout()
        {
            Content = LoginPattern.App.GetLoginPage(this).ConvertPageToUIElement(this);
        }
    }
}
