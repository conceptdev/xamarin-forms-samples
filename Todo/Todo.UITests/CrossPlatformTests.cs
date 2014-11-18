using System;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using System.Linq;

namespace Todo.UITests
{
	public class CrossPlatformTests
	{
		protected IApp _app;

		bool IsAndroid {
			get {
				return _app is Xamarin.UITest.Android.AndroidApp;
			}
		}

		bool IsiOS {
			get {
				return _app is Xamarin.UITest.iOS.iOSApp;
			}
		}

		// toolbar - (BUG) can't tap these in iOS
//		static readonly Func<AppQuery, AppQuery> Add = c => c.Marked("+");
//		static readonly Func<AppQuery, AppQuery> SpeakAll = c => c.Marked("?");
		static readonly Func<AppQuery, AppQuery> ToolbarAdd = c => c.Marked("ToolbarAdd");
		static readonly Func<AppQuery, AppQuery> SpeakAll = c => c.Marked("ToolbarSpeak");

		// floating button
		static readonly Func<AppQuery, AppQuery> AddButton = c => c.Marked("TodoAdd");

		// form elements
		static readonly Func<AppQuery, AppQuery> TodoName = c => c.Marked("TodoName");
		static readonly Func<AppQuery, AppQuery> TodoNotes = c => c.Marked("TodoNotes");
		static readonly Func<AppQuery, AppQuery> TodoDone = c => c.Marked("TodoDone");
		static readonly Func<AppQuery, AppQuery> Save = c => c.Marked("TodoSave");
		static readonly Func<AppQuery, AppQuery> Delete = c => c.Marked("TodoDelete");
		static readonly Func<AppQuery, AppQuery> Speak = c => c.Marked("TodoSpeak");

		/// <summary>
		/// We do 'Ignore' in the base class so that these set of tests aren't actually *run*
		/// outside the context of one of the platform-specific bootstrapper sub-classes.
		/// </summary>
		[SetUp]
		public virtual void SetUp()
		{
			Assert.Ignore ("This class requires a platform-specific bootstrapper to override the `SetUp` method");
		}


		#region Helpers

		void TapAndDelete(string cellText) {
			_app.Tap (j => j.Marked (cellText));
			_app.Tap (Speak);
			_app.Tap (Delete);
		}

		void AddFromList (string name, string notes) {
			_app.Tap (AddButton);

			_app.WaitForElement (TodoName);
			_app.Tap (TodoName);
			_app.EnterText (name);
			_app.Tap (TodoNotes);
			_app.EnterText (notes);
			_app.Tap (Save); 
		}

		#endregion

		[Test ()]
		public void TestDelete ()
		{
			// Arrange 
			AppResult[] result = _app.Query (a => a.Marked ("Buy pears"));
			Assert.IsTrue(result.Any(), "Expected there to be a 'buy pears' row to delete; remove the app from the simulator and re-run test.");

			// Act
			TapAndDelete ("Buy pears");

			// Assert
			result = _app.Query(a => a.Marked ("Buy pears"));
			Assert.IsTrue(result.Length == 0, "The 'buy pears' row wasn't deleted :-(");
		}

		[Test ()]
		public void TestAdd ()
		{
			// Arrange 
			AppResult[] result = _app.Query (a => a.Marked ("Buy kale"));
			Assert.IsTrue(result.Length == 0, "There should not already be a 'buy kale' task");

			// Act
			AddFromList ("Buy kale", "organic"); 
			_app.WaitForElement (AddButton); // back on listView page

			// Assert
			result = _app.Query(a => a.Marked ("Buy kale"));
			Assert.IsTrue(result.Any(), "The 'buy kale' row wasn't added :-(");
		}

		[Test (), Category("DodgyTests")]
		public void DoAll ()
		{
			// Arrange - Nothing to do because the queries have already been initialized.
			Assert.IsTrue(true, "Noop, this is for demo purposes only :-) ");

			// Act
			var aq = _app.Query (a => a.Marked ("Buy pears"));
			if (aq.Length > 0) { // exists 
				TapAndDelete ("Buy pears");


				_app.Tap (j => j.Marked ("Buy orange"));
				_app.Tap (Speak);
				_app.Tap (Delete);

				_app.Tap (j => j.Marked ("Buy milk"));
				_app.Tap (Speak);
				_app.Tap (Delete);

				_app.Tap (j => j.Marked ("Buy mangos"));
				_app.Tap (Speak);
				_app.Tap (Delete);

				_app.Tap (j => j.Marked ("Buy apples"));
				_app.Tap (Speak);
				_app.Tap (Delete);

				_app.Tap (j => j.Marked ("Buy bananas"));
				_app.Tap (Speak);
				_app.Tap (Delete);
			}



			AddFromList ("Buy kale", "organic"); 
			AddFromList ("Buy broccolini", "organic"); 
			AddFromList ("Buy dragonfruit", "organic"); 

			if (IsAndroid) {
				_app.Tap (SpeakAll); // on toolbar (BUG can't tap on iOS)
			}

			// Assert
			var result = _app.Query(z => z.Marked ("Buy dragonfruit"));
			Assert.IsTrue(result.Any(), "The 'dragonfruit' task wasn't added :-(");
		}
	}
}

