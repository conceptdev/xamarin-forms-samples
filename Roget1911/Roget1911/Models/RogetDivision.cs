using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Roget1911
{
	/// <summary>
	/// I'm not really sure what a 'Division' is, linguistically
	/// </summary>
	public class RogetDivision
    {
        public RogetDivision()
        {
            Sections = new List<RogetSection>();
        }

        [XmlAttribute]
        public string Name { set; get; }
		
        public List<RogetSection> Sections { get; set; }

    }
}
