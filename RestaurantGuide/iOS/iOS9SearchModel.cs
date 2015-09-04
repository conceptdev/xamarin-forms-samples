using System;
using System.Collections.Generic;
using System.Linq;
using CoreSpotlight;
using MobileCoreServices;

namespace RestaurantGuide.iOS
{
	/// <summary>
	/// courtesy of Larry O'Brien
	/// </summary>
	public class iOS9SearchModel
	{
		//readonly Dictionary<Guid, string> searchIndexMap;
		readonly Dictionary<Guid, Restaurant> searchIndexMap2;

		/// <returns>Restaurant Name</returns>
		public string Lookup (Guid g) {
			var r = from s in searchIndexMap2
			        where s.Key == g
			        select s.Value;
			return r.FirstOrDefault ().Name;
		}

		public iOS9SearchModel (List<Restaurant> restaurants)
		{
			searchIndexMap2 = new Dictionary<Guid, Restaurant> ();
			foreach (var r in restaurants) {
				searchIndexMap2.Add (Guid.NewGuid (), r);
			}
			var dataItems = searchIndexMap2.Select (keyValuePair => {
				var guid = keyValuePair.Key;
				var restaurant = keyValuePair.Value;

				var attributeSet = new CSSearchableItemAttributeSet (UTType.Text);
				attributeSet.Title = restaurant.Name;
				attributeSet.ContentDescription = restaurant.Text;
				attributeSet.TextContent = restaurant.Text;

				var dataItem = new CSSearchableItem (guid.ToString (), "com.xamarin.restguide", attributeSet);
				return dataItem;

			});

//			searchIndexMap = new Dictionary<Guid, string> ();
//			foreach (var r in restaurants) {
//				searchIndexMap.Add (Guid.NewGuid (), r.Name);
//			}
//
//			//CoreSpotlight initialization
//			var dataItems = searchIndexMap.Select (keyValuePair => {
//				var guid = keyValuePair.Key;
//				var restaurant = keyValuePair.Value;
//
//				var attributeSet = new CSSearchableItemAttributeSet (UTType.Text);
//				attributeSet.Title = restaurant;
//				attributeSet.ContentDescription = "My app's data relating to " + restaurant;
//				attributeSet.TextContent = restaurant;
//
//				var dataItem = new CSSearchableItem (guid.ToString (), "com.xamarin.restguide", attributeSet);
//				return dataItem;
//			
//			});

			// HACK: index should be 'managed' rather than deleted/created each time
			CSSearchableIndex.DefaultSearchableIndex.DeleteAll(null);

			CSSearchableIndex.DefaultSearchableIndex.Index (dataItems.ToArray<CSSearchableItem> (), err => {
				if (err != null) {
					Console.WriteLine (err);
				} else {
					Console.WriteLine ("Indexed items successfully");
				}
			});
		}
	}
}

