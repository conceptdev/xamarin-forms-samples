using System;
using Foundation;

namespace RestaurantGuide.iOS
{
	/// <summary>
	/// Constants for the NSUserActivity, also embedded in Info.plist
	/// </summary>
	public static class ActivityTypes
	{
		public const string View = "co.conceptdev.restguide.activity.view";
	}

	/// <summary>
	/// Extension methods for NSUserActivity
	/// </summary>
	public static class ActivityKeys {
		public static NSString Id = new NSString ("id");
		public static NSString Name = new NSString ("name");


		public static bool IsIndexable (this Restaurant current){
			return (current != null && current.Name != null && current.Cuisine != null && current.Chef != null);
		}
		public static NSDictionary NameToDictionary (this Restaurant current){
			return NSDictionary.FromObjectAndKey (new NSString (current.Name), ActivityKeys.Name);
		}
		public static NSDictionary IdToDictionary (this Restaurant current){
			return NSDictionary.FromObjectAndKey (new NSString (current.Number.ToString ()), ActivityKeys.Id);
		}
	}
	public static class UserActivityHelper
	{
		public static NSUserActivity CreateNSUserActivity(Restaurant userInfo)
		{
			var activityType = ActivityTypes.View;
			var activity = new NSUserActivity(activityType);
			activity.EligibleForSearch = true; // HACK: can result in duplicates with CoreSpotlight
			activity.EligibleForPublicIndexing = false;
			activity.EligibleForHandoff = false;

			activity.Title = "Restaurant " + userInfo.Name;

			//			var keywords = new NSString[] {new NSString("Add"), new NSString("Todo"), new NSString("Empty"), new NSString("Task") };
			//			activity.Keywords = new NSSet<NSString>(keywords);

			var attributeSet = new CoreSpotlight.CSSearchableItemAttributeSet ();

			attributeSet.DisplayName = userInfo.Name;
			attributeSet.ContentDescription = userInfo.Cuisine + " " + userInfo.Chef;

			// Handoff https://developer.apple.com/library/ios/documentation/UserExperience/Conceptual/Handoff/AdoptingHandoff/AdoptingHandoff.html
//			attributeSet.RelatedUniqueIdentifier = userInfo.Number.ToString(); // CoreSpotlight "id"


			activity.AddUserInfoEntries(NSDictionary.FromObjectAndKey(new NSString(userInfo.Number.ToString()), ActivityKeys.Id));

			activity.ContentAttributeSet = attributeSet;

			activity.BecomeCurrent (); // don't forget to ResignCurrent()

			return activity;
		}
	}
}

