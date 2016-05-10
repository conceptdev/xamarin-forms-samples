using System;
using System.Linq;
using Xamarin.Forms;

namespace  Todo
{
	/// <summary>
	/// These are mainly for iOS - there are more traits but for now just experimenting with two
	/// </summary>
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
			BindableProperty.CreateAttached("AccessibilityLabel", typeof(string), typeof(AccessibilityEffect), "", propertyChanged:OnAccessibilityLabelChanged);
		
		public static readonly BindableProperty AccessibilityHintProperty =
			BindableProperty.CreateAttached("AccessibilityHint", typeof(string), typeof(AccessibilityEffect), "", propertyChanged:OnAccessibilityHintChanged);

		public static readonly BindableProperty AccessibilityIDProperty =
			BindableProperty.CreateAttached("AccessibilityID", typeof(string), typeof(AccessibilityEffect), "", propertyChanged:OnAccessibilityIDChanged);
		
		public static readonly BindableProperty InAccessibleTreeProperty =
			BindableProperty.CreateAttached("InAccessibleTree", typeof(bool), typeof(AccessibilityEffect), true, propertyChanged: OnInAccessibleTreeChanged);

		public static readonly BindableProperty AccessibilityTraitsProperty =
			BindableProperty.CreateAttached("AccessibilityTraits", typeof(AccessibilityTrait), typeof(AccessibilityEffect), AccessibilityTrait.None, propertyChanged:OnAccessibilityTraitsChanged);
		
		#region AccessibilityLabel
		public static string GetAccessibilityLabel(BindableObject view)
		{
			return (string)view.GetValue (AccessibilityLabelProperty);
		}
		public static void SetAccessibilityLabel(BindableObject view, string value)
		{
			view.SetValue (AccessibilityLabelProperty, value);
		}
		static void OnAccessibilityLabelChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var hasLabel = !string.IsNullOrEmpty(newValue as string);
			AddRemoveEffect(bindable, hasLabel);
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
		static void OnAccessibilityHintChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var hasHint = !string.IsNullOrEmpty(newValue as string);
			AddRemoveEffect(bindable, hasHint);
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
		static void OnAccessibilityIDChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var hasId = !string.IsNullOrEmpty(newValue as string);
			AddRemoveEffect(bindable, hasId);
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
		static void OnInAccessibleTreeChanged(BindableObject bindable, object oldValue, object newValue)
		{
			//var inTree = (bool)newValue;
			var add = newValue != null;
			AddRemoveEffect(bindable, add);
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
		static void OnAccessibilityTraitsChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var traits = (AccessibilityTrait)newValue;
			var hasTraits = traits == AccessibilityTrait.None;
			AddRemoveEffect(bindable, hasTraits);
		}
		#endregion

		/// <summary>
		/// Adds or removes the effect on the control
		/// </summary>
		/// <remarks>
		/// So... I'm still figuring out the semantics for when to remove
		/// and what the most efficient way is to check before adding.
		/// </remarks>
		static void AddRemoveEffect(BindableObject bindable, bool add)
		{
			var view = bindable as View;
			if (view == null)
			{
				return;
			}
			if (add)
			{
				if (view.Effects.Count == 0)
				{
					// shortcut to add if there are none already
					view.Effects.Add(new AddAccessibilityEffect());	
				}
				else 
				{ 
					// more expensive check to see if it exists before adding
					var exists = view.Effects.First(e => e is AddAccessibilityEffect);
					if (exists == null)
					{ 
						view.Effects.Add(new AddAccessibilityEffect());
					}
				}

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

		public class AddAccessibilityEffect : RoutingEffect
		{
			public AddAccessibilityEffect() : base("MyCompany.AddAccessibilityEffect")
			{
			}
		}
	}
}

