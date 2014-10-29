using System;
using Xamarin.Forms;
using System.Diagnostics;
using BugSweeper.WinPhone;

[assembly: Xamarin.Forms.Dependency (typeof (PlaySound_WinPhone))]

namespace BugSweeper.WinPhone
{
	public class PlaySound_WinPhone : IPlaySound
	{
		#region IPlaySound implementation

		public void PlayLaugh ()
		{
			Debug.WriteLine ("todo");
		}

		#endregion
	}
}

