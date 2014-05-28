using System;
using System.Threading.Tasks;
using PCLStorage;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Diagnostics;

namespace Roget1911
{
	public class XmlLoader
	{
		/// <summary>'Class'es are the root list of items in the hierarchy</summary>
		public List<RogetClass> Classes = new List<RogetClass>();
		/// <summary>The main data-set of words by part-of-speech</summary>
		public RogetCategories Categories;

		public bool IsLoaded = false;

		RogetHierarchy hierarchy;

		public async Task LoadXml() {

			if (IsLoaded)
				return;

			IFolder rootfolder = FileSystem.Current.LocalStorage;
			Debug.WriteLine (rootfolder.Path);
			IFile file = await rootfolder.GetFileAsync ("roget15aHierarchy.xml");
			using (TextReader reader = new StreamReader(await file.OpenAsync(FileAccess.Read)))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(RogetHierarchy));
				hierarchy = (RogetHierarchy)serializer.Deserialize(reader);
				// HACK: makes Divisions synonymous with Sections, makes navigation easier
				foreach (var h in hierarchy.Classes)
				{
					foreach (RogetDivision d in h.Divisions)
					{
						h.Sections.Add(new RogetSection{Name=d.Name, Sections = d.Sections});
					}
				}
			} 
			Classes = hierarchy.Classes;

			file = await rootfolder.GetFileAsync ("roget15aCategories.xml");
			using (TextReader reader = new StreamReader(await file.OpenAsync(FileAccess.Read)))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(RogetCategories));
				Categories = (RogetCategories)serializer.Deserialize(reader);
			}
			IsLoaded = true;
			return;
		}
	}
}

