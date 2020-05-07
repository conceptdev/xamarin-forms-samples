using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesView : ContentView
    {
        public NotesView()
        {
            InitializeComponent();
        }
        void OnNoteAddedClicked(object sender, EventArgs e)
        {
            MainPage.Current.OnNoteAddedClicked(sender, e);
            //await Navigation.PushAsync(new NoteEntryPage
            //{
            //    BindingContext = new Note()
            //});
        }

        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                MainPage.Current.OnListViewItemSelected(e.SelectedItem as Note);
                //await Navigation.PushAsync(new NoteEntryPage
                //{
                //    BindingContext = e.SelectedItem as Note
                //});
            }
        }
    }
}