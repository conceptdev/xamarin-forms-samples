using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace RestaurantGuide.WinPhone81
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage 
    {
        public MainPage()
        {
            this.InitializeComponent();

            var t = LoadXml();
            t.Wait();
            var x = t.Result;
            RestaurantGuide.App.SetContent(x);

            LoadApplication(new RestaurantGuide.App());

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        async Task<List<Restaurant>> LoadXml()
        {
            var restaurants = new List<Restaurant>();
            #region load data from XML

            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            try
            {
                var uri = new System.Uri("ms-appx:///restaurants.xml");
                var f = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(uri).AsTask().ConfigureAwait(false);
                var stream = await f.OpenStreamForReadAsync().ConfigureAwait(false);

                var serializer = new XmlSerializer(typeof(List<Restaurant>));

                restaurants = (List<Restaurant>)serializer.Deserialize(stream.AsInputStream().AsStreamForRead());
            }
            catch (Exception e)
            {
                Debug.WriteLine("cannot open " + e);
            }

            #endregion
            return restaurants;
        }   



        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }
    }
}
