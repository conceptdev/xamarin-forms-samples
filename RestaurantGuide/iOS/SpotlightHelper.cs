using System;
using System.Collections.Generic;
using System.Linq;
using CoreSpotlight;
using MobileCoreServices;

namespace RestaurantGuide.iOS
{
	public class SpotlightHelper
	{
		/// <returns>Restaurant Name</returns>
		public string Lookup (string num) {
			var res = from r in restaurants
					where r.Number == Convert.ToInt32(num)
					select r;
			var f = res.FirstOrDefault();
			if (f != null) {
				return f.Name;
			} else {
				return Random (); // HACK: deal with bad data from NSUserActivity (or CoreSpotlight)
			}
		}
		public string Random () {
			var r = new Random ();
			var rn = r.Next (0, restaurants.Count - 1);
			Console.WriteLine ($"Random number {rn} in {restaurants.Count}");
			return restaurants[rn].Name;
		}
		List<Restaurant> restaurants;
		public SpotlightHelper (List<Restaurant> restaurants)
		{
			this.restaurants = restaurants;
			var dataItems = new List<CSSearchableItem>();
			foreach (var r in restaurants) {
				var attributeSet = new CSSearchableItemAttributeSet (UTType.Text);
				attributeSet.Title = r.Name;
				attributeSet.ContentDescription = r.Cuisine;
				attributeSet.TextContent = r.Chef;

				var dataItem = new CSSearchableItem (r.Number.ToString(), "com.xamarin.restguide", attributeSet);
				dataItems.Add (dataItem);
			}

			// HACK: index should be 'managed' rather than deleted/created each time - keep track of what's indexed?
			// see the "To9o" sample for a better user-input search indexing strategy
			CSSearchableIndex.DefaultSearchableIndex.DeleteAll(null);

			CSSearchableIndex.DefaultSearchableIndex.Index (dataItems.ToArray<CSSearchableItem> (), err => {
				if (err != null) {
					Console.WriteLine (err);
					Xamarin.Insights.Report(new Exception("CoreSpotlight Index Failed"), new Dictionary <string, string> { 
						{"Message", err.ToString()}
					}, Xamarin.Insights.Severity.Error);
				} else {
					Console.WriteLine ("Indexed items successfully");
					Xamarin.Insights.Track("CoreSpotlight", new Dictionary<string, string> {
						{"Type", "Indexed successfully"}
					});
				}
			});
		}
	}
}

