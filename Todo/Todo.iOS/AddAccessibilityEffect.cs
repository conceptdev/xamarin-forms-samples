using System;
using Todo;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("MyCompany")]
[assembly: ExportEffect(typeof(AddAccessibilityEffect), "AddAccessibilityEffect")]

namespace Todo
{
	/// <summary>
	/// Add accessibility properties to Xamarin.Forms controls in iOS
	/// </summary>
	public class AddAccessibilityEffect : PlatformEffect
	{
		protected override void OnAttached()
		{
			try
			{
				Control.AccessibilityLabel = AccessibilityEffect.GetAccessibilityLabel(Element);
				Control.AccessibilityHint = AccessibilityEffect.GetAccessibilityHint(Element);
				Control.AccessibilityIdentifier = AccessibilityEffect.GetAccessibilityID(Element);
				Control.IsAccessibilityElement = AccessibilityEffect.GetInAccessibleTree(Element);
				SetTraits();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
			}
		}

		protected override void OnDetached()
		{
		}

		protected override void OnElementPropertyChanged(System.ComponentModel.PropertyChangedEventArgs args)
		{
			if (args.PropertyName == AccessibilityEffect.AccessibilityLabelProperty.PropertyName)
			{
				Control.AccessibilityLabel = AccessibilityEffect.GetAccessibilityLabel(Element);
			}
			else if (args.PropertyName == AccessibilityEffect.AccessibilityHintProperty.PropertyName)
			{
				Control.AccessibilityHint = AccessibilityEffect.GetAccessibilityHint(Element);
			}
			else if (args.PropertyName == AccessibilityEffect.AccessibilityIDProperty.PropertyName)
			{
				Control.AccessibilityIdentifier = AccessibilityEffect.GetAccessibilityID(Element);
			}
			else if (args.PropertyName == AccessibilityEffect.InAccessibleTreeProperty.PropertyName)
			{
				Control.IsAccessibilityElement = AccessibilityEffect.GetInAccessibleTree(Element);
			}
			else if (args.PropertyName == AccessibilityEffect.AccessibilityTraitsProperty.PropertyName)
			{
				SetTraits();
			}
			else 
			{
				base.OnElementPropertyChanged(args);
			}
		}

		void SetTraits() 
		{ 
			var tempTraits = Control.AccessibilityTraits;

			var newTraits = AccessibilityEffect.GetAccessibilityTraits(Element);

			if ((newTraits & AccessibilityTrait.Button) > 0) tempTraits = tempTraits | UIAccessibilityTrait.Button;

			if ((newTraits & AccessibilityTrait.Header) > 0) tempTraits = tempTraits | UIAccessibilityTrait.Header;

			Control.AccessibilityTraits = tempTraits;
		}

	}
}

