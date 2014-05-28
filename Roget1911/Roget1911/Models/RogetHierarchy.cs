using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Roget1911
{
    public class RogetHierarchy
    {
        public RogetHierarchy()
        {
            Classes = new List<RogetClass>();
        }

        [XmlAttribute]
        public string Name { set; get; }
		
        public List<RogetClass> Classes { set; get; }
    }
}