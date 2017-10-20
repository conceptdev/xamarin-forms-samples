using System;
using System.Linq;
using System.Collections.Generic;

namespace Xamarin.Forms {
	
	/// <summary>
	/// New WrapLayout
	/// </summary>
	/// <author>Jason Smith</author>
	/// <author>Tobias Theel</author>
	public class WrapLayout : Layout<View> {
	    readonly Dictionary<View, SizeRequest> _layoutCache = new Dictionary<View, SizeRequest>();

	    /// <summary>
	    /// Backing Storage for the Spacing property
	    /// </summary>
	    public static readonly BindableProperty SpacingProperty = BindableProperty.Create(nameof(Spacing), typeof(double), typeof(WrapLayout), 5d, BindingMode.OneWay, null, OnPropertyChanged);

	    private static void OnPropertyChanged(BindableObject bindableObject, object oldValue, object newValue) 
	    {
	      ((WrapLayout)bindableObject)._layoutCache.Clear();
	    }

	    /// <summary>
	    /// Spacing added between elements (both directions)
	    /// </summary>
	    /// <value>The spacing.</value>
	    public double Spacing 
	    {
	      get { return (double)GetValue(SpacingProperty); }
	      set { SetValue(SpacingProperty, value); }
	    }

	    public WrapLayout() 
	    {
	      this.HorizontalOptions = LayoutOptions.FillAndExpand;
	      this.VerticalOptions = this.HorizontalOptions;
	    }

	    protected override void OnChildMeasureInvalidated() {
	      base.OnChildMeasureInvalidated();
	      this._layoutCache.Clear();
	    }

	    protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint) {
	      ICollection<ICollection<Tuple<View, Rectangle>>> layout = NaiveLayout(widthConstraint, out double lastX, out double lastY);
	      return new SizeRequest(new Size(lastX, lastY));
	    }

	    protected override void LayoutChildren(double x, double y, double width, double height) {
	      ICollection<ICollection<Tuple<View, Rectangle>>> layout = NaiveLayout(width, out double lastX, out double lastY);

	      foreach (ICollection<Tuple<View, Rectangle>> t in layout) 
	      {
		int offset = (int)((width - t.Last().Item2.Right) / 2);
		foreach (Tuple<View, Rectangle> dingus in t) 
		{
		  Rectangle location = new Rectangle(dingus.Item2.X + x + offset, dingus.Item2.Y + y, dingus.Item2.Width, dingus.Item2.Height);
		  LayoutChildIntoBoundingRegion(dingus.Item1, location);
		}
	      }
	    }

	    private ICollection<ICollection<Tuple<View, Rectangle>>> NaiveLayout(double width, out double lastX, out double lastY) 
	    {
	      double startX = 0;
	      double startY = 0;
	      double right = width;
	      double nextY = 0;

	      lastX = 0;
	      lastY = 0;

	      ICollection<ICollection<Tuple<View, Rectangle>>> result = new List<ICollection<Tuple<View, Rectangle>>>();
	      ICollection<Tuple<View, Rectangle>> currentList = new List<Tuple<View, Rectangle>>();

	      foreach (View child in this.Children) 
	      {
		if (!this._layoutCache.TryGetValue(child, out SizeRequest sizeRequest)) 
		{
		  SizeRequest request = sizeRequest = child.Measure(double.PositiveInfinity, double.PositiveInfinity);
		  this._layoutCache[child] = request;
		}

		double paddedWidth = sizeRequest.Request.Width + this.Spacing;
		double paddedHeight = sizeRequest.Request.Height + this.Spacing;

		if (startX + paddedWidth > right) {
		  startX = 0;
		  startY += nextY;

		  if (currentList.Count > 0) {
		    result.Add(currentList);
		    currentList = new List<Tuple<View, Rectangle>>();
		  }
		}

		currentList.Add(new Tuple<View, Rectangle>(child, new Rectangle(startX, startY, sizeRequest.Request.Width, sizeRequest.Request.Height)));

		lastX = Math.Max(lastX, startX + paddedWidth);
		lastY = Math.Max(lastY, startY + paddedHeight);

		nextY = Math.Max(nextY, paddedHeight);
		startX += paddedWidth;
	      }
	      result.Add(currentList);
	      return result;
   	 }
  	}



	/// <summary>
	/// Simple Layout panel which performs wrapping on the boundaries.
	/// </summary>
	public class WrapLayoutOld : Layout<View>
	{
		/// <summary>
		/// Backing Storage for the Orientation property
		/// </summary>
		public static readonly BindableProperty OrientationProperty = 
			BindableProperty.Create<WrapLayoutOld, StackOrientation> (w => w.Orientation, StackOrientation.Vertical, 
				propertyChanged: (bindable, oldvalue, newvalue) => ((WrapLayoutOld)bindable).OnSizeChanged ());

		/// <summary>
		/// Orientation (Horizontal or Vertical)
		/// </summary>
		public StackOrientation Orientation {
			get { return (StackOrientation)GetValue (OrientationProperty); }
			set { SetValue (OrientationProperty, value); } 
		}

		/// <summary>
		/// Backing Storage for the Spacing property
		/// </summary>
		public static readonly BindableProperty SpacingProperty = 
			BindableProperty.Create<WrapLayoutOld, double> (w => w.Spacing, 6, 
				propertyChanged: (bindable, oldvalue, newvalue) => ((WrapLayoutOld)bindable).OnSizeChanged());

		/// <summary>
		/// Spacing added between elements (both directions)
		/// </summary>
		/// <value>The spacing.</value>
		public double Spacing {
			get { return (double)GetValue (SpacingProperty); }
			set { SetValue (SpacingProperty, value); } 
		}

		/// <summary>
		/// This is called when the spacing or orientation properties are changed - it forces
		/// the control to go back through a layout pass.
		/// </summary>
		private void OnSizeChanged()
		{
			this.ForceLayout();
		}

		//http://forums.xamarin.com/discussion/17961/stacklayout-with-horizontal-orientation-how-to-wrap-vertically#latest
//		protected override void OnPropertyChanged
//		(string propertyName = null)
//		{
//			base.OnPropertyChanged(propertyName);
//			if ((propertyName == WrapLayout.OrientationProperty.PropertyName) ||
//				(propertyName == WrapLayout.SpacingProperty.PropertyName)) {
//				this.OnSizeChanged();
//			}
//		}

		/// <summary>
		/// This method is called during the measure pass of a layout cycle to get the desired size of an element.
		/// </summary>
		/// <param name="widthConstraint">The available width for the element to use.</param>
		/// <param name="heightConstraint">The available height for the element to use.</param>
		protected override SizeRequest OnSizeRequest (double widthConstraint, double heightConstraint)
		{
			if (WidthRequest > 0)
				widthConstraint = Math.Min (widthConstraint, WidthRequest);
			if (HeightRequest > 0)
				heightConstraint = Math.Min (heightConstraint, HeightRequest);

			double internalWidth = double.IsPositiveInfinity (widthConstraint) ? double.PositiveInfinity : Math.Max (0, widthConstraint);
			double internalHeight = double.IsPositiveInfinity (heightConstraint) ? double.PositiveInfinity : Math.Max (0, heightConstraint);

			return Orientation == StackOrientation.Vertical 
				? DoVerticalMeasure(internalWidth, internalHeight) 
					: DoHorizontalMeasure(internalWidth, internalHeight);

		}

		/// <summary>
		/// Does the vertical measure.
		/// </summary>
		/// <returns>The vertical measure.</returns>
		/// <param name="widthConstraint">Width constraint.</param>
		/// <param name="heightConstraint">Height constraint.</param>
		private SizeRequest DoVerticalMeasure(double widthConstraint, double heightConstraint)
		{
			int columnCount = 1;

			double width = 0;
			double height = 0;
			double minWidth = 0;
			double minHeight = 0;
			double heightUsed = 0;

			foreach (var item in Children)    
			{
				var size = item.GetSizeRequest(widthConstraint, heightConstraint);
				width = Math.Max (width, size.Request.Width);

				var newHeight = height + size.Request.Height + Spacing;
				if (newHeight > heightConstraint) {
					columnCount++;
					heightUsed = Math.Max(height, heightUsed);
					height = size.Request.Height;
				} else
					height = newHeight;

				minHeight = Math.Max(minHeight, size.Minimum.Height);
				minWidth = Math.Max (minWidth, size.Minimum.Width);
			}

			if (columnCount > 1) {
				height = Math.Max(height, heightUsed);
				width *= columnCount;  // take max width
			}

			return new SizeRequest(new Size(width, height), new Size(minWidth,minHeight));
		}

		/// <summary>
		/// Does the horizontal measure.
		/// </summary>
		/// <returns>The horizontal measure.</returns>
		/// <param name="widthConstraint">Width constraint.</param>
		/// <param name="heightConstraint">Height constraint.</param>
		private SizeRequest DoHorizontalMeasure(double widthConstraint, double heightConstraint)
		{
			int rowCount = 1;

			double width = 0;
			double height = 0;
			double minWidth = 0;
			double minHeight = 0;
			double widthUsed = 0;

			foreach (var item in Children)    
			{
				var size = item.GetSizeRequest(widthConstraint, heightConstraint);
				height = Math.Max (height, size.Request.Height);

				var newWidth = width + size.Request.Width + Spacing;
				if (newWidth > widthConstraint) {
					rowCount++;
					widthUsed = Math.Max(width, widthUsed);
					width = size.Request.Width;
				} else
					width = newWidth;

				minHeight = Math.Max(minHeight, size.Minimum.Height);
				minWidth = Math.Max (minWidth, size.Minimum.Width);
			}

			if (rowCount > 1) {
				width = Math.Max(width, widthUsed);
				height = (height + Spacing) * rowCount - Spacing; // via MitchMilam 
			}

			return new SizeRequest(new Size(width, height), new Size(minWidth,minHeight));
		}

		/// <summary>
		/// Positions and sizes the children of a Layout.
		/// </summary>
		/// <param name="x">A value representing the x coordinate of the child region bounding box.</param>
		/// <param name="y">A value representing the y coordinate of the child region bounding box.</param>
		/// <param name="width">A value representing the width of the child region bounding box.</param>
		/// <param name="height">A value representing the height of the child region bounding box.</param>
		protected override void LayoutChildren (double x, double y, double width, double height)
		{
			if (Orientation == StackOrientation.Vertical) {
				double colWidth = 0;
				double yPos = y, xPos = x;

				foreach (var child in Children.Where(c => c.IsVisible)) 
				{
					var request = child.GetSizeRequest (width, height);

					double childWidth = request.Request.Width;
					double childHeight = request.Request.Height;
					colWidth = Math.Max(colWidth, childWidth);

					if (yPos + childHeight > height) {
						yPos = y;
						xPos += colWidth + Spacing;
						colWidth = 0;
					}

					var region = new Rectangle (xPos, yPos, childWidth, childHeight);
					LayoutChildIntoBoundingRegion (child, region);
					yPos += region.Height + Spacing;
				}
			}
			else {
				double rowHeight = 0;
				double yPos = y, xPos = x;

				foreach (var child in Children.Where(c => c.IsVisible)) 
				{
					var request = child.GetSizeRequest (width, height);

					double childWidth = request.Request.Width;
					double childHeight = request.Request.Height;
					rowHeight = Math.Max(rowHeight, childHeight);

					if (xPos + childWidth > width) {
						xPos = x;
						yPos += rowHeight + Spacing;
						rowHeight = 0;
					}

					var region = new Rectangle (xPos, yPos, childWidth, childHeight);
					LayoutChildIntoBoundingRegion (child, region);
					xPos += region.Width + Spacing;
				}

			}
		}
	}
}
