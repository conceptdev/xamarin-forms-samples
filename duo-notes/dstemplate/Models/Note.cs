using System;

namespace dstemplate
{
    public class Note
    {
        public string Filename { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public bool CanDelete
        {
            get { return !string.IsNullOrEmpty(Filename); }
        }
    }
}
