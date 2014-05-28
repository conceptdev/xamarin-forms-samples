using System;
using System.Linq;
using System.Xml.Linq; //need to update References for MonoDevelop solution
using System.Collections.Generic;
using System.Diagnostics;

namespace Roget1911
{
	public class RogetCategories
    {
        public RogetCategories()
        {
            Categories = new List<RogetCategory>();
        }
		
        public List<RogetCategory> Categories { set; get; }
		
		/// <summary>
		/// The Hierarchy xml specifies sections with 'ranges'.
		/// The ranges are mostly numeric, but occasionally have an 'a' suffix,
		/// hence the extension method to allow the linq query select the range
		/// using numeric comparison (we treat 16 and 16a as equivalent)
		/// </summary>
		public List<RogetCategory> GetRange (string start, string end)
		{	
			Debug.WriteLine("Get {0} to {1}", start, end);
			var l = from c in Categories
				where c.Index >= start.ToNumber() && c.Index <= end.ToNumber()
				select c;
			return l.ToList();
		}
		
	
    }
	public static class RogetExtensions
	{
		/// <summary>
		/// HACK: pretty specific to this application :)
		/// </summary>
		public static int ToNumber(this string num)
		{
			string s = num.Replace("a","");
			int index;
			if (int.TryParse(s, out index))
				return index;
			else
				return -1;
		}
	}
}
