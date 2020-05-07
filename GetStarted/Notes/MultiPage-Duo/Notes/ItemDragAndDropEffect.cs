using System;
using Xamarin.Forms;

namespace Notes
{
    public class ItemDragAndDropEffect : RoutingEffect
    {
        public string Uri { get; set; }

        public string Description { get; set; }

        public ItemDragAndDropEffect() : base("Notes.ItemDragAndDropEffect")
        {

        }
    }
}