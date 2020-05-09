using System;
using System.Linq;
using Android.Content;
using Notes.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AView = Android.Widget.AdapterView;
using LView = Android.Widget.ListView;
using ADragFlags = Android.Views.DragFlags;
using AUri = Android.Net.Uri;
using Android.Widget;

[assembly: ResolutionGroupName("Notes")]
[assembly: ExportEffect(typeof(ItemDragAndDropEffect), "ItemDragAndDropEffect")]
namespace Notes.Droid.Effects
{
    public class ItemDragAndDropEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            (Control as AView).OnItemLongClickListener = new ItemLongClickListen(this);
            (Control as LView).LongClickable = true;
            //Control.SetBackgroundColor(Android.Graphics.Color.LightGreen); // HACK: show it's bound
        }

        protected override void OnDetached()
        {
            if (Control != null && Control.Handle != IntPtr.Zero)
                (Control as AView).OnItemLongClickListener = null;
        }

        public class ItemLongClickListen : Java.Lang.Object, AView.IOnItemLongClickListener
        {
            ItemDragAndDropEffect _dragAndDropEffect;

            public ItemLongClickListen(ItemDragAndDropEffect dragAndDropEffect)
            {
                _dragAndDropEffect = dragAndDropEffect;
            }

            public bool OnItemLongClick(AView parent, Android.Views.View v, int position, long id)
            {
                if (v.Handle == IntPtr.Zero)
                    return false;

                var dragAndDropEffect = (Notes.ItemDragAndDropEffect)
                        _dragAndDropEffect.Element.Effects.FirstOrDefault(e => e is Notes.ItemDragAndDropEffect);

                // get the Note
                var itemContent = (parent as LView).GetItemAtPosition(position).ToString();
                
                var data = ClipData.NewPlainText(new Java.Lang.String("Note"), new Java.Lang.String(itemContent));

                var dragShadowBuilder = new AView.DragShadowBuilder(v);

                v.StartDragAndDrop(data, dragShadowBuilder, v, (int)ADragFlags.Global | (int)ADragFlags.GlobalUriRead | (int)ADragFlags.GlobalUriWrite);
                return true;
            }
        }
    }
}