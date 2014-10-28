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
	public class iOSTest : Tests
	{
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
	}
}

