using Notes.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            if (!DeviceIsSpanned)
            { // single-screen
                if (twoPaneView.PanePriority == TwoPaneViewPriority.Pane2)
                { //showing detail, back goes to master (list)
                    twoPaneView.PanePriority = TwoPaneViewPriority.Pane1;
                    return true;
                }
            }
            return base.OnBackButtonPressed();
        }

        public bool DeviceIsSpanned => DualScreenInfo.Current.SpanMode != TwoPaneViewMode.SinglePane;

        public void UpdateLayouts()
        {
            if (twoPaneView.Pane1 == null) return;

            if (DeviceIsSpanned)
            {
                twoPaneView.WideModeConfiguration = TwoPaneViewWideModeConfiguration.LeftRight;
                twoPaneView.TallModeConfiguration = TwoPaneViewTallModeConfiguration.TopBottom;
            }
            else
            {   // single screen!
                twoPaneView.WideModeConfiguration = TwoPaneViewWideModeConfiguration.SinglePane;
                twoPaneView.TallModeConfiguration = TwoPaneViewTallModeConfiguration.SinglePane;
            }
        }
    }
}