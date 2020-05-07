using Notes.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.DualScreen;
using Xamarin.Forms.Xaml;

namespace Notes
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public static MainPage Current { get; private set; }
        public MainPage()
        {
            InitializeComponent();
            twoPaneView.LayoutChanged += OnTwoPaneViewLayoutChanged;
            Current = this;
        }

        private void OnTwoPaneViewLayoutChanged(object sender, EventArgs e)
        {
            UpdateLayouts();
        }

        public void OnNoteAddedClicked(object sender, EventArgs args)
        {
            twoPaneView.Pane2 = new NotesEntryView()
            {
                BindingContext = new Note()
            };
            twoPaneView.PanePriority = TwoPaneViewPriority.Pane2;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshData();
        }

        public void RefreshData()
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
            //listView.ItemsSource = notes
            //    .OrderBy(d => d.Date)
            //    .ToList();
        }

        public void OnListViewItemSelected(Note note)
        {
            twoPaneView.Pane2 = new NotesEntryView()
            {
                BindingContext = note
            };
            twoPaneView.PanePriority = TwoPaneViewPriority.Pane2;
        }

        protected override bool OnBackButtonPressed()
        {
            return true; // TODO: update this later
            if (twoPaneView.PanePriority == TwoPaneViewPriority.Pane1)
            {
                //
            }
            if (twoPaneView.PanePriority == TwoPaneViewPriority.Pane2)
            {
                if (twoPaneView.Pane2?.GetType() == typeof(NotesEntryView))
                {
                    twoPaneView.Pane1 = new NotesView();
                    twoPaneView.PanePriority = TwoPaneViewPriority.Pane1;
                    RefreshData();
                    return true;
                }
            }
            return false;
        }

        public bool DeviceIsSpanned => DualScreenInfo.Current.SpanMode != TwoPaneViewMode.SinglePane;

        public void UpdateLayouts()
        {
            if (twoPaneView.Pane1 == null) return;

            //twoPaneView.MinTallModeHeight = 0;
            //twoPaneView.MinWideModeWidth = 600;
            //twoPaneView.Pane1Length = new GridLength(1, GridUnitType.Star);
            //twoPaneView.Pane2Length = new GridLength(1, GridUnitType.Star);
            //twoPaneView.TallModeConfiguration = TwoPaneViewTallModeConfiguration.TopBottom;
            //twoPaneView.WideModeConfiguration = TwoPaneViewWideModeConfiguration.LeftRight;

            //if (DeviceIsSpanned)
            //{
            //    twoPaneView.TallModeConfiguration = TwoPaneViewTallModeConfiguration.TopBottom;
            //    twoPaneView.WideModeConfiguration = TwoPaneViewWideModeConfiguration.LeftRight;
            //}
            //else
            //{
            //    twoPaneView.TallModeConfiguration = TwoPaneViewTallModeConfiguration.SinglePane;
            //    twoPaneView.WideModeConfiguration = TwoPaneViewWideModeConfiguration.SinglePane;
            //}
            
        }
    }
}