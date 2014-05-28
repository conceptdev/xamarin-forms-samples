using System;
using System.Collections.Generic;
using System.Collections;
using SQLite.Net.Attributes;

namespace Evolve13
{
	public class Session
	{
		public Session ()
		{
			Speakers = new List<Speaker>();
		}

		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Title {get;set;}
		public string Abstract {get;set;}
		public string Location {get;set;}
		public DateTime Begins {get;set;}
		public DateTime Ends {get;set;}

		public string LocationDisplay {
			get {
				if (Location.ToLower ().Contains ("tbd") 
				    || Location.ToLower ().Contains ("room")
				    || Location.ToLower ().Contains ("hall")
				    || Title.ToLower ().Contains ("party"))
					return Location;
				else
					return Location + " room";
			}
		}

		// compat
		public string Code { get { return Id.ToString (); } }
		public DateTime Start { get { return Begins; } }
		public DateTime End { get { return Ends; } }
		public string DateTimeDisplay {get{ return Begins.ToString("ddd MMM dd H:mm");}}
		public string DateTimeQuickJumpDisplay {
			get{return Begins.ToString("ddd MMM dd H:mm");}}
		public string DateTimeQuickJumpSubtitle {
			get{return Begins.ToString("ddd MMM dd H:mm");}}
        public string TimeQuickJumpDisplay{
            get{ return Start.ToString("ddd H:mm");}}
		public bool HasTag (string tag)
		{
			return false;
		}

		[Ignore]
		public List<Speaker> Speakers {get;set;}

		//[Ignore]
		//public string SpeakerList { get { return GetSpeakerList();  } }

	}
}