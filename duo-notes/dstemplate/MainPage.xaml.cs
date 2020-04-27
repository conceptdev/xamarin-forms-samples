using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
            _pane1 = new NotesView();
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
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Refresh();
        }

        public void Refresh()
        {
            var notes = new List<Note>();

            var files = Directory.EnumerateFiles(App.FolderPath, "*.notes.txt");
            foreach (var filename in files)
            {
                notes.Add(new Note
                {
                    Filename = filename,
                    Text = File.ReadAllText(filename),
                    Date = File.GetCreationTime(filename)
                });
            }

            BindingContext = notes
                .OrderByDescending(d => d.Date)
                .ToList();

            if (_pane1?.GetType() == typeof(NotesView))
            {
                (_pane1 as NotesView).BindingContext = notes
                    .OrderByDescending(d => d.Date)
                    .ToList();
                (_pane1 as NotesView).IsBusy = false;
                twoPaneView.PanePriority = TwoPaneViewPriority.Pane1;
            }
        }

        public void OnNoteAddedClicked(object sender, EventArgs e)
        {
            Pane2 = new NotesEntryView() {
                BindingContext = new Note()
            };
            twoPaneView.PanePriority = TwoPaneViewPriority.Pane2;
        }

        public void OnItemSelected(Note note)
        {
            Pane2 = new NotesEntryView()
            {
                BindingContext = note
            };
            twoPaneView.PanePriority = TwoPaneViewPriority.Pane2;
        }

        protected override bool OnBackButtonPressed()
        {
            if (twoPaneView.PanePriority == TwoPaneViewPriority.Pane1)
            {
                if (_pane1?.GetType() == typeof(SettingsView))
                {
                    Pane1 = new NotesView();
                    Refresh();
                    return true;
                }
            }
            if (twoPaneView.PanePriority == TwoPaneViewPriority.Pane2)
            {
                if (_pane2?.GetType() == typeof(NotesEntryView))
                {
                    //Pane1 = new NotesView();
                    twoPaneView.PanePriority = TwoPaneViewPriority.Pane1;
                    Refresh();
                    return true;
                }
            }
            return false;
        }
    }
}
