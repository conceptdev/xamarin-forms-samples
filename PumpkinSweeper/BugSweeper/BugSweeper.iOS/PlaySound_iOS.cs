using System;
using Xamarin.Forms;
using System.Diagnostics;
using AVFoundation;
using BugSweeper.iOS;
using Foundation;
using System.Collections.Generic;


[assembly: Xamarin.Forms.Dependency (typeof (PlaySound_iOS))]

namespace BugSweeper.iOS
{
	public class PlaySound_iOS : IPlaySound
	{
		#region IPlaySound implementation

		public void PlayLaugh ()
		{
			// http://xamapp.com/playing-sound-with-xamarin-ios/
			var soundurl = NSUrl.FromFilename("laugh.mp3"); // http://soundbible.com/2055-Evil-Laugh-Male-6.html
			var onePlay = AVAudioPlayer.FromUrl(soundurl);
			//var onePlay = player[playIndex];
			onePlay.CurrentTime = 4;// 4 secs; onePlay.Duration*3;
			onePlay.NumberOfLoops = 1;
			onePlay.Volume = 1.0f;
//			onePlay.FinishedPlaying += DidFinishPlaying;
			onePlay.PrepareToPlay();
			onePlay.Play();


			try {
				// HACK: just for Xamarin Insights testing - PlatformNotSupportedException
				throw new InvalidOperationException ("IPlaySound iOS 'invalid'");
			} 
			catch (Exception exception) { 
				exception.Data["Type"] = "Pretend sound was not implemented";
//				exception.Data["Alarm"] = "Bad char \a";	// BREAKS
//				exception.Data["Long"] = "0123456789112345678921234567893123456789412345678951234567896123456789712345678981234567899123456789A123456789B123456789C123456789D123456789E123456789F123456789";
//				exception.Data["0123456789112345678921234567893123456789412345678951234567896123456789712345678981234567899123456789A123456789B123456789C123456789D123456789E123456789F123456789"] = "was long?";
//				exception.Data["Japanese"] = "レストラン–料理店–飲食店";
//				exception.Data["Korean"] = "레스토랑–레스토랑–요정";
//				exception.Data["Hebrew"] = "מסעדה";
//				exception.Data["ChineseT"] ="餐廳–飯店";
//				exception.Data["ChineseS"] ="餐厅–酒家";
//				exception.Data["レストラン–料理店–飲食店"] = "レストラン–料理店–飲食店";
//				exception.Data["레스토랑–레스토랑–요정"] = "레스토랑–레스토랑–요정";
//				exception.Data["מסעדה"] = "asdf מסעד";
//				exception.Data["餐廳–飯店"] ="餐廳–飯店";
//				exception.Data["餐厅–酒家"] ="餐厅–酒家";

				Xamarin.Insights.Report(exception, new Dictionary <string, string> { 
					{"Some additional info", "0123456789112345678921234567893123456789412345678951234567896123456789712345678981234567899123456789A123456789B123456789C123456789D123456789E123456789F123456789"},
//					{"Newline", "line 1\nline 2"}, //OK
//					{"Newline", "line 1"+Environment.NewLine+"line 2"}, //OK
//					{"Null", "null \0"}, //OK
//					{"Backspace", "bs \b"}, //OK
//					{"Alarm", "alarm \a"}, //BREAKS - report does not appear in dashboard
//					{"Japanese", "レストラン–料理店–飲食店"},
//					{"Korean", "레스토랑–레스토랑–요정"},
//					{"Hebrew", "מסעדה"},
//					{"ChineseT","餐廳–飯店"},
//					{"ChineseS","餐厅–酒家"},
//					{"レストラン–料理店–飲食店", "レストラン–料理店–飲食店レストラン–料理店–飲食店レストラン–料理店–飲食店レストラン–料理店–飲食店レストラン–料理店–飲食店レストラン–料理店–飲食店レストラン–料理店–飲食店"},
//					{"레스토랑–레스토랑–요정", "레스토랑–레스토랑–요정레스토랑–레스토랑–요정레스토랑–레스토랑–요정레스토랑–레스토랑–요정레스토랑–레스토랑–요정레스토랑–레스토랑–요정레스토랑–레스토랑–요정레스토랑–레스토랑–요정레스토랑–레스토랑–요정"},
//					{"餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店","餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店"},
//					{"餐厅–酒家","餐厅–酒家–餐厅–酒家–餐厅–酒家–餐厅–酒家–餐厅–酒家–餐厅–酒家–餐厅–酒家–餐厅–酒家–餐厅–酒家餐厅–酒家餐厅–酒家餐厅–酒家餐厅–酒家餐厅–酒家餐厅–酒家餐厅–酒家"},
				}, Xamarin.Insights.Severity.Error);



			}
		}

		#endregion
	}
}

