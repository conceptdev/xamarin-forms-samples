using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace dstemplate
{
    public partial class NotesView : ContentView, INotifyPropertyChanged
    {
        public NotesView()
        {
            InitializeComponent();
        }

        void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //string previous = (e.PreviousSelection.FirstOrDefault() as Note)?.Filename;
            //string current = (e.CurrentSelection.FirstOrDefault() as Note)?.Filename;

            var note = (e.CurrentSelection.FirstOrDefault() as Note) ?? new Note();

            App._mainPage.OnItemSelected(note);
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            App._mainPage.Pane1 = new SettingsView();
        }

        void AddTapGestureRecognizer_Tapped (System.Object sender, System.EventArgs e)
        {
            App._mainPage.OnNoteAddedClicked(sender, e);
        }

        //bool _isBusy = false;
        //public bool IsBusy
        //{
        //    get => _isBusy;
        //    set {
        //        _isBusy = value;
        //        OnPropertyChanged("IsBusy");
        //    }
        //}   
    }
}
