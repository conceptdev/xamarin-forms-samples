using System;
using Xamarin.Forms;
using EmployeeDirectory.Data;
using System.Threading.Tasks;
using System.Linq;
using System.IO;

namespace EmployeeDirectory
{
	public static class App
	{
		/// <summary>
		/// service used to supply data to the app
		/// </summary>
		/// <remarks>
		/// * Memory (uses CSV file)
		/// * LDAP (requires network)
		/// </remarks>
		public static IDirectoryService Service;

		/// <summary>
		/// last time the device was used
		/// </summary>
		public static DateTime LastUseTime { get; set; }

		public static Page GetMainPage ()
		{
			//
			// Create the service
			//

			//
			// Local CSV file
//			Service = await MemoryDirectoryService.FromCsv ("XamarinDirectory.csv");

			//
			// LDAP service - uncomment to try it out.
			//Service = new LdapDirectoryService {
			//	Host = "ldap.mit.edu",
			//	SearchBase = "dc=mit,dc=edu",
			//};

			var employeeList = new EmployeeListXaml ();

			var mainNav = new NavigationPage (employeeList);

			return mainNav;
		}
	}
}

