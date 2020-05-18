using System;
using System.Linq;
using Android.Content;
using dstemplate.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AView = Android.Views.View;
using ADragFlags = Android.Views.DragFlags;
using AUri = Android.Net.Uri;

[assembly: ResolutionGroupName("dstemplate")]
[assembly: ExportEffect(typeof(DragAndDropEffect), "DragAndDropEffect")]
namespace dstemplate.Droid.Effects
{
    public class DragAndDropEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            Control.SetOnLongClickListener(new LongClickListen(this));
            Control.SetOnClickListener(new ClickListen(this));
        }

        protected override void OnDetached()
        {
            if (Control != null && Control.Handle != IntPtr.Zero)
            {
                Control.SetOnLongClickListener(null);
                Control.SetOnClickListener(null);
            }
        }
        public class ClickListen : Java.Lang.Object, AView.IOnClickListener
        {
            DragAndDropEffect _dragAndDropEffect;

            public ClickListen(DragAndDropEffect dragAndDropEffect)
            {
                _dragAndDropEffect = dragAndDropEffect;
            }

            public void OnClick(AView v)
            {
                if (v.Handle == IntPtr.Zero)
                    return;

                Android.Views.View parent = (Android.Views.View)v.Parent;
                ((Android.Views.View)parent.Parent).PerformClick();
            }
        }
        public class LongClickListen : Java.Lang.Object, AView.IOnLongClickListener
        {
            DragAndDropEffect _dragAndDropEffect;

            public LongClickListen(DragAndDropEffect dragAndDropEffect)
            {
                _dragAndDropEffect = dragAndDropEffect;
            }

            public bool OnLongClick(AView v)
            {
                if (v.Handle == IntPtr.Zero)
                    return false;

                var dragAndDropEffect = (dstemplate.DragAndDropEffect)
                        _dragAndDropEffect.Element.Effects.FirstOrDefault(e => e is dstemplate.DragAndDropEffect);

                var itemContent = (v as Android.Widget.TextView).Text;

                //var data = ClipData.NewPlainText(new Java.Lang.String("Note"), new Java.Lang.String(itemContent));
                var data = ClipData.NewHtmlText(new Java.Lang.String("Note"), new Java.Lang.String(itemContent), $"<b>{itemContent}</b>");
                var dragShadowBuilder = new AView.DragShadowBuilder(v);

                v.StartDragAndDrop(data, dragShadowBuilder, v, (int)ADragFlags.Global | (int)ADragFlags.GlobalUriRead | (int)ADragFlags.GlobalUriWrite);
                return true;
            }
        }
    }
}