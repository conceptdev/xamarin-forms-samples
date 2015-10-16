using System;
using Xamarin.Forms;
using Android.Media;
using BugSweeper.Droid;
using System.Collections.Generic;

[assembly: Xamarin.Forms.Dependency (typeof (PlaySound_Android))]

namespace BugSweeper.Droid
{
	public class PlaySound_Android : IPlaySound
	{
		#region IPlaySound implementation

		public void PlayLaugh ()
		{
			// http://developer.xamarin.com/recipes/android/media/audio/play_audio/
			MediaPlayer _player;
			_player = MediaPlayer.Create(Forms.Context, Resource.Raw.laugh); // http://soundbible.com/2055-Evil-Laugh-Male-6.html
			_player.Start ();

			try {
				// HACK: just for Xamarin Insights testing
				throw new PlatformNotSupportedException ("IPlaySound Android");
			} 
			catch (Exception exception) { 
				exception.Data["Type"] = "Pretend sound was not implemented";
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
//					{"0123456789112345678921234567893123456789412345678951234567896123456789712345678981234567899123456789A123456789B123456789C123456789D123456789E123456789F123456789","was long?"},
//					{" Japanese", "レストラン–料理店–飲食店"},
//					{" Korean", "레스토랑–레스토랑–요정"},
//					{" Hebrew", "מסעדה"},
//					{" ChineseT","餐廳–飯店"},
//					{" ChineseS","餐厅–酒家"},
//					{"レストラン–料理店–飲食店", "レストラン–料理店–飲食店レストラン–料理店–飲食店レストラン–料理店–飲食店レストラン–料理店–飲食店レストラン–料理店–飲食店レストラン–料理店–飲食店レストラン–料理店–飲食店"},
//					{"레스토랑–레스토랑–요정", "레스토랑–레스토랑–요정레스토랑–레스토랑–요정레스토랑–레스토랑–요정레스토랑–레스토랑–요정레스토랑–레스토랑–요정레스토랑–레스토랑–요정레스토랑–레스토랑–요정레스토랑–레스토랑–요정레스토랑–레스토랑–요정"},
//					{" Hמסעדew", "מסעדמסעדמסעדמסעדמסעדמסעדה"},
//					{"餐廳–飯店","餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店餐廳飯店"},
//					{"餐厅–酒家","餐厅–酒家–餐厅–酒家–餐厅–酒家–餐厅–酒家–餐厅–酒家–餐厅–酒家–餐厅–酒家–餐厅–酒家–餐厅–酒家餐厅–酒家餐厅–酒家餐厅–酒家餐厅–酒家餐厅–酒家餐厅–酒家餐厅–酒家"},
				}, Xamarin.Insights.Severity.Error);
			}
		}

		#endregion
	}
}

