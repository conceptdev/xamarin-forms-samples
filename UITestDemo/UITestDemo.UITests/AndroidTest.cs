using System;
using NUnit.Framework;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;
using System.Reflection;
using System.IO;
using Xamarin.UITest;
using System.Linq;

namespace UITestDemo.UITests
{
	[TestFixture ()]
	public class AndroidTest : Tests
	{
	
		public string PathToAPK { get; set; }


		[TestFixtureSetUp]
		public void TestFixtureSetup()
		{
			string currentFile = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
			FileInfo fi = new FileInfo(currentFile);
			string dir = fi.Directory.Parent.Parent.Parent.FullName;
			PathToAPK = Path.Combine(dir, "Android", "bin", "Debug", "UITestDemo.Android.apk");
		}

		[SetUp]
		public void SetUp()
		{
			// an API key is required to publish on Xamarin Test Cloud for remote, multi-device testing
			_app = ConfigureApp.Android.ApkFile(PathToAPK).ApiKey("YOUR_API_KEY_HERE").StartApp();
		}
	}
}
