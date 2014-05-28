using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Roget1911
{
	public class RogetClass
	{
		public RogetClass () 
		{
			Divisions = new List<RogetDivision>();
			Sections = new List<RogetSection>();
		}
		
		[XmlAttribute]
		public string Name { get; set; }
		
		public List<RogetSection> Sections { get; set; }
		
		private List<RogetDivision> divisions;
		/// <summary>
		/// HACK: treat Divisions like Sections - I don't understand the difference right now.
		/// See the deserialization code in MainViewController.cs
		/// </summary>
		public List<RogetDivision> Divisions { get;set;}
	}
}
