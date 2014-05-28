using System;
using System.Collections.Generic;

namespace RestaurantGuide
{
	public class Restaurant
	{
		public int Number {get;set;}
		public string Name {get;set;}
		public string Url {get; set;}

		public string Cuisine { get; set; }
		public string Address {get;set;}
		public string Phone {get;set;}
		public string Website {get;set;}
		public string Text {get;set;}

		public string Hours {get;set;}
		public string CreditCards { get; set; }
		public string Chef { get; set; }

		public string StartsWith
		{
			get
			{
				return Name[0].ToString();
			}
		}

		public override string ToString()
		{
			return String.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10}"
				,Number, Name, Url, Cuisine, Address, Phone, Website, Text, Hours, CreditCards, Chef);
		}
	}
}