using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RestaurantGuide.WinApp
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
        }
        
            async Task<List<Restaurant>> LoadXml()
            {
                var restaurants = new List<Restaurant>();
                #region load data from XML

                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            
                try {
                    var uri = new System.Uri("ms-appx:///restaurants.xml");
                    var f = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(uri).AsTask().ConfigureAwait(false);
                    var stream = await f.OpenStreamForReadAsync().ConfigureAwait(false);

                    var serializer = new XmlSerializer(typeof(List<Restaurant>));

                    restaurants = (List<Restaurant>)serializer.Deserialize(stream.AsInputStream().AsStreamForRead());
                }
                catch (Exception e) {
                    Debug.WriteLine("cannot open " + e);
                }
            
                #endregion
                return restaurants;
            }
    }
}
