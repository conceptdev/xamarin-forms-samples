using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Roget1911
{
	public class RogetPartOfSpeech
	{
		public RogetPartOfSpeech ()
		{
			Lines = new List<string>();
		}

		[XmlAttribute]
		public PartOfSpeech PartOfSpeech {get;set;}
	
		public List<string> Lines {get;set;}
	}
}
