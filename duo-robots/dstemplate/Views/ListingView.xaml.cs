using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace dstemplate
{
    public partial class ListingView : ContentView, INotifyPropertyChanged
    {
        public ListingView()
        {
            InitializeComponent();

            BindingContext = this;
        }

        void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string previous = (e.PreviousSelection.FirstOrDefault() as CellModel)?.Username;
            string current = (e.CurrentSelection.FirstOrDefault() as CellModel)?.Username;

            var cm = (e.CurrentSelection.FirstOrDefault() as CellModel) ?? new CellModel();

            App._mainPage.Pane2 = new DetailView {
                BindingContext = cm
            };
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            App._mainPage.Pane1 = new SettingsView();
        }

        bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set {
                _isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        public List<CellModel> Cells
        {
            get
            {
                return new List<CellModel>
                {
                    new CellModel { Username="Robot", Comment="Lost in Space", Date="1965", Url="https://en.wikipedia.org/wiki/Robot_(Lost_in_Space)"},
                    new CellModel { Username="Dexter", Comment="Perfect Match", Date="1984", Url="https://cv.vic.gov.au/stories/creative-life/tv50-anniversary-of-television-in-australia/dexter-the-robot/"},
                    new CellModel { Username="Number 5", Comment="Short Circuit", Date="1986", Url="https://en.wikipedia.org/wiki/Short_Circuit_(1986_film)"},
                    new CellModel { Username="HAL 9000", Comment="2001: A Space Odyessy", Date="1968", Url="https://en.wikipedia.org/wiki/HAL_9000"},
                    new CellModel { Username="Giskard", Comment="Robots and Empire", Date="1985", Url="https://asimov.fandom.com/wiki/R._Giskard_Reventlov"},
                    new CellModel { Username="Data", Comment="Star Trek", Date="1987", Url="https://en.wikipedia.org/wiki/Data_(Star_Trek)"},
                    new CellModel { Username="Robby", Comment="Forbidden Planet", Date="1956", Url="https://en.wikipedia.org/wiki/Robby_the_Robot"},
                };
            }
        }
    }

    public class CellModel
    {
        public string Username { get; set; }
        //public string Avatar { get; set; }
        public string Comment { get; set; }
        public string Date { get; set; }
        public string Url { get; set; }
    }
}
