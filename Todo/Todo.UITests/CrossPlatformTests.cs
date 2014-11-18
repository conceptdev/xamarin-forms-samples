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

		// toolbar
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
		#endregion

		[Test ()]
		public void TestCase ()
		{
			//_app.Repl ();

//			var x = _app.Query ();
//			var y = _app.Query (g => g.Marked ("TodoAdd"));
//			var z = _app.Query (h => h.Marked ("TodoList"));
//			var a = _app.Query (i => i.Marked ("CellLabel"));

			// Arrange - Nothing to do because the queries have already been initialized.
//			AppResult[] result = _app.Query(InitialMessage);
//			Assert.IsTrue(result.Any(), "The initial message string isn't correct - maybe the app wasn't re-started?");
//
//			// Act
//			_app.Tap(Add);

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

//			_app.Tap (ToolbarAdd); // on toolbar (BUG)
			_app.Tap (AddButton); // on floating image button
			_app.Tap (TodoName);
			_app.EnterText ("Buy kale");
			_app.Tap (TodoNotes);
			_app.EnterText ("organic");
			_app.Tap (TodoDone);
			_app.Tap (Save); 




			_app.Tap (AddButton);
			_app.Tap (TodoName);
			_app.EnterText ("Buy broccolini");
			_app.Tap (TodoNotes);
			_app.EnterText ("organic");
			//_app.Tap (TodoDone);
			_app.Tap (Save); 



			_app.Tap(AddButton);
			_app.Tap (TodoName);
			_app.EnterText ("Buy dragonfruit");
			_app.Tap (TodoNotes);
			_app.EnterText ("organic");
			_app.Tap (TodoDone);
			_app.Tap (Save); 



//			_app.Tap (SpeakAll); // on toolbar (BUG)


//			// Assert
//			result = _app.Query(DoneMessage);
//			Assert.IsTrue(result.Any(), "The 'clicked' message is not being displayed.");
		}
	}
}

