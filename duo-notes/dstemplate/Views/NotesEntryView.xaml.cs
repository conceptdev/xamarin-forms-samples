using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace dstemplate
{
    public partial class NotesEntryView : ContentView
    {
        public NotesEntryView()
        {
            InitializeComponent();
        }
        
        void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;

            if (string.IsNullOrWhiteSpace(note.Filename))
            {
                // Save
                var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.notes.txt");
                File.WriteAllText(filename, note.Text);
            }
            else
            {
                // Update 
                File.WriteAllText(note.Filename, note.Text);
            }

            UpdateNotes();
        }

        void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;

            if (File.Exists(note.Filename))
            {
                File.Delete(note.Filename);
            }

            UpdateNotes();
        }

        void UpdateNotes()
        {
            App._mainPage.Refresh();
        }
    }
}
