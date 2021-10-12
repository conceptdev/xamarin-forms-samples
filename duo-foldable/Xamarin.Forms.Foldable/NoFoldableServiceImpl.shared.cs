using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Foldable
{
	internal class NoFoldableServiceImpl : IFoldableService
	{
		static Lazy<NoFoldableServiceImpl> _Instance = new Lazy<NoFoldableServiceImpl>(() => new NoFoldableServiceImpl());
		public static NoFoldableServiceImpl Instance => _Instance.Value;

		readonly WeakEventManager _onScreenChangedEventManager = new WeakEventManager();
		public NoFoldableServiceImpl()
		{
			Device.info.PropertyChanged += OnDeviceInfoChanged;

		}

		public Task<int> GetHingeAngleAsync() => Task.FromResult(0);

		public bool IsSpanned => false;

		public bool IsLandscape => Device.info.CurrentOrientation.IsLandscape();

		public DeviceInfo DeviceInfo => Device.info;

		public event EventHandler OnScreenChanged
		{
			add { _onScreenChangedEventManager.AddEventHandler(value); }
			remove { _onScreenChangedEventManager.RemoveEventHandler(value); }
		}

		public void Dispose()
		{
		}

		public Size ScaledScreenSize => Device.info.ScaledScreenSize;
		public Rectangle GetHinge()
		{
			return Rectangle.Zero;
		}

		public Point? GetLocationOnScreen(VisualElement visualElement)
		{
			return null;
		}

		public object WatchForChangesOnLayout(VisualElement visualElement, Action action)
		{
			if (action == null)
				return null;

			EventHandler<EventArg<VisualElement>> layoutUpdated = (_, __) =>
			{
				action();
			};

			visualElement.BatchCommitted += layoutUpdated;
			return layoutUpdated;
		}

		public void StopWatchingForChangesOnLayout(VisualElement visualElement, object handle)
		{
			if (handle is EventHandler<EventArg<VisualElement>> handler)
				visualElement.BatchCommitted -= handler;
		}

		void OnDeviceInfoChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			_onScreenChangedEventManager.HandleEvent(this, e, nameof(OnScreenChanged));
		}
	}
}