using NUnit.Framework;
using System;
using Xamarin.UITest.Queries;
using System.Reflection;
using System.IO;
using Xamarin.UITest;
using Xamarin.UITest.iOS;
using System.Linq;

namespace UITestDemo.UITests
{
	[TestFixture ()]
	public class iOSTest
	{

		static readonly Func<AppQuery, AppQuery> Button = c => c.Marked("MyButton");
		static readonly Func<AppQuery, AppQuery> DoneMessage = c => c.Marked("MyLabel").Text("Was clicked");

		iOSApp _app;

		public string PathToIPA { get; set; }


		[TestFixtureSetUp]
		public void TestFixtureSetup()
		{
			string currentFile = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
			FileInfo fi = new FileInfo(currentFile);
			string dir = fi.Directory.Parent.Parent.Parent.FullName;
			PathToIPA = Path.Combine(dir, "iOS", "bin", "iPhoneSimulator", "Debug", "UITestDemoiOS.app");
		}

		[SetUp]
		public void SetUp()
		{
			// an API key is required to publish on Xamarin Test Cloud for remote, multi-device testing
			_app = ConfigureApp.iOS.AppBundle(PathToIPA).ApiKey("YOUR_API_KEY_HERE").StartApp();
		}


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

