using System;
using System.Linq;
using Xamarin.Forms;

namespace  Todo
{
	[Flags]
	public enum AccessibilityTrait { 
		None = 0,
		Button,
		Header
	}

	/// <summary>
	/// Add accessibility properties to Xamarin.Forms controls
	/// </summary>
	public static class AccessibilityEffect
	{
		public static readonly BindableProperty AccessibilityLabelProperty =
					BindableProperty.CreateAttached("AccessibilityLabel", typeof(string), typeof(AccessibilityEffect), "");
		
		public static readonly BindableProperty AccessibilityHintProperty =
					BindableProperty.CreateAttached("AccessibilityHint", typeof(string), typeof(AccessibilityEffect), "");

		public static readonly BindableProperty AccessibilityIDProperty =
					BindableProperty.CreateAttached("AccessibilityID", typeof(string), typeof(AccessibilityEffect), "");
		
		public static readonly BindableProperty InAccessibleTreeProperty =
					BindableProperty.CreateAttached("InAccessibleTree", typeof(bool), typeof(AccessibilityEffect), true);

		public static readonly BindableProperty AccessibilityTraitsProperty =
					BindableProperty.CreateAttached("AccessibilityTraits", typeof(AccessibilityTrait), typeof(AccessibilityEffect), AccessibilityTrait.None);
		
		public static readonly BindableProperty IsAccessibleProperty =
					BindableProperty.CreateAttached("IsAccessible", typeof(bool), typeof(AccessibilityEffect), false, propertyChanged: OnIsAccessibleChanged);

		#region AccessibilityLabel
		public static string GetAccessibilityLabel(BindableObject view)
		{
			return (string)view.GetValue (AccessibilityLabelProperty);
		}
		public static void SetAccessibilityLabel(BindableObject view, string value)
		{
			view.SetValue (AccessibilityLabelProperty, value);
		}
		#endregion

		#region AccessibilityHint
		public static string GetAccessibilityHint(BindableObject view)
		{
			return (string)view.GetValue(AccessibilityHintProperty);
		}
		public static void SetAccessibilityHint(BindableObject view, string value)
		{
			view.SetValue(AccessibilityHintProperty, value);
		}
		#endregion

		#region AccessibilityID
		public static string GetAccessibilityID(BindableObject view)
		{
			return (string)view.GetValue(AccessibilityIDProperty);
		}
		public static void SetAccessibilityID(BindableObject view, string value)
		{
			view.SetValue(AccessibilityIDProperty, value);
		}
		#endregion

		#region InAccessibleTree
		public static bool GetInAccessibleTree(BindableObject view)
		{
			return (bool)view.GetValue(InAccessibleTreeProperty);
		}
		public static void SetInAccessibleTree(BindableObject view, bool value)
		{
			view.SetValue(InAccessibleTreeProperty, value);
		}
		#endregion

		#region AccessibilityTraits
		public static AccessibilityTrait GetAccessibilityTraits(BindableObject view)
		{
			return (AccessibilityTrait)view.GetValue(AccessibilityTraitsProperty);
		}
		public static void SetAccessibilityTraits(BindableObject view, AccessibilityTrait value)
		{
			view.SetValue(AccessibilityTraitsProperty, value);
		}
		#endregion

		#region IsAccessible
		public static bool GetIsAccessible(BindableObject view)
		{
			return (bool)view.GetValue(IsAccessibleProperty);
		}
		public static void SetIsAccessible(BindableObject view, bool value)
		{
			view.SetValue(IsAccessibleProperty, value);
		}

		static void OnIsAccessibleChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var view = bindable as View;
			if (view == null)
			{
				return;
			}

			bool isAccessible = (bool)newValue;
			if (isAccessible)
			{
				view.Effects.Add(new AddAccessibilityEffect());
			}
			else
			{
				var toRemove = view.Effects.FirstOrDefault(e => e is AddAccessibilityEffect);
				if (toRemove != null)
				{
					view.Effects.Remove(toRemove);
				}
			}
		}
		#endregion

		public class AddAccessibilityEffect : RoutingEffect
		{
			public AddAccessibilityEffect() : base("MyCompany.AddAccessibilityEffect")
			{
			}
		}
	}
}

