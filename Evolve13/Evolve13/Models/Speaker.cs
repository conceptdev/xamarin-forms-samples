using System;
using System.Collections.Generic;
using SQLite;
using Xamarin.Forms;

namespace Evolve13
{
	/* Example Json
{"id":0,"name":"Chris Hardy","twitterHandle":"chrisntr","bio":"Chris Hardy, a Microsoft ASPInsider, is an .NET consultant focusing on MonoTouch and Mono for Android development.\n\nEver since MonoTouch was in beta, Chris has been developing and evangelising MonoTouch and Mono for Android and was one of the first users to get a MonoTouch application on to the App Store. Speaking at conferences around the world on the subject, Chris has been a key part of the community and extended this by contributing to MonoTouch and Mono for Android books.","headshotUrl":"/images/speakers/chris.jpg"}
	 */
	public class Speaker
	{
		public Speaker ()
		{
			Sessions = new List<Session>();
		}

		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Name {get;set;}
		public string TwitterHandle {get;set;}
		public string Bio {get;set;}
		public string HeadshotUrl {get;set;}

		[Ignore]
		public List<Session> Sessions {get;set;}

		[Ignore]
		public bool HasTwitter {
			get{ 
				return !String.IsNullOrWhiteSpace (TwitterHandle) && (Device.OS == TargetPlatform.iOS);
			}
		}
	}
}

