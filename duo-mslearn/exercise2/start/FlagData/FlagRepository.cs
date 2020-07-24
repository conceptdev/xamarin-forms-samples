using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace FlagData
{
    /// <summary>
    /// Repository holding all our flags; this could be a
    /// web service client pulling data, but to keep this local
    /// with no Internet requirements, we've put the data into this
    /// assembly.
    /// </summary>
    public class FlagRepository
    {
        public IList<string> Countries { get; }

        /// <summary>
        /// The list of available flags
        /// </summary>
        public IList<Flag> Flags { get; }

        /// <summary>
        /// Locate a country by the name.
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public Flag FindByCountry(string country)
        {
            return Flags
                .FirstOrDefault(f => string.Compare(f.Country, country, StringComparison.OrdinalIgnoreCase) == 0);
        }

        /// <summary>
        /// Return the list of countries.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetCountries()
        {
            return Flags.Select(f => f.Country);
        }

        /// <summary>
        /// Constructor - loads the flags
        /// </summary>
        public FlagRepository()
        {
            var dataAssembly = GetType().GetTypeInfo().Assembly;

            // Get our XML data.
            var data = XElement.Load(dataAssembly
                        .GetManifestResourceStream("FlagData.Data.flags.xml"));

            // Turn the XML into Flag objects with LINQ to XML.
            var flags = data.Elements("flag")
                .Select(f =>
                   new Flag {
                       Country = f.Attribute("country").Value,
                       ImageUrl = "FlagData.Images." + f.Attribute("imageUrl").Value,
                       DateAdopted = (DateTime)f.Attribute("adopted"),
                       IncludesShield = (bool)f.Attribute("hasShield"),
                       MoreInformationUrl = new Uri(f.Attribute("url").Value),
                       Description = f.Value.Trim()
                   });

            Flags = new List<Flag>(flags.OrderBy(f => f.Country));

            // Read the countries in.
            Countries = new List<string>();
            using (var sr = new StreamReader(dataAssembly.GetManifestResourceStream("FlagData.Data.countries.csv")))
            {
                while (!sr.EndOfStream)
                {
                    string entry = sr.ReadLine();
                    if (!string.IsNullOrEmpty(entry))
                        Countries.Add(entry);
                }
            }
        }
    }
}
