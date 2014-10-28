using System;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using System.Linq;

namespace UITestDemo.UITests
{
	public class Tests
	{
		protected IApp _app;

		static readonly Func<AppQuery, AppQuery> Button = c => c.Marked("MyButton");
		static readonly Func<AppQuery, AppQuery> DoneMessage = c => c.Marked("MyLabel").Text("Was clicked");


		[Test ()]
		public void TestCase ()
		{
			// Arrange - Nothing to do because the queries have already been initialized.

			// Act
			_app.Tap(Button);

			// Assert
			AppResult[] result = _app.Query(DoneMessage);
			Assert.IsTrue(result.Any(), "The 'clicked' message is not being displayed.");
		}
	}
}

