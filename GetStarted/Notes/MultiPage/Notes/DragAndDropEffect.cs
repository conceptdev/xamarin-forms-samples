using System;
using Xamarin.Forms;

namespace Notes
{
    public class DragAndDropEffect : RoutingEffect
    {
        public string Uri { get; set; }

        public string Description { get; set; }

        public DragAndDropEffect() : base("Notes.DragAndDropEffect")
        {

        }
    }
}