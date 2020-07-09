using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace GameOfLife.FileManagement
{
	public static class FileReadWrite
	{

		// Writes the information in the given LifeRuleset rules to the XML file with path defined by path
		// May throw typical FileStream creation or read errors
		public static void WriteLifeRulesetToXMLFile(LifeRuleset rules, string path)
		{
			try
			{
				LifeRuleset item = new LifeRuleset();
				item.InitializeForSerialization(rules.GetGrowthArray(), rules.GetLivingArray(), rules.GetDeathArray());
				XmlSerializer write = new XmlSerializer(typeof(LifeRuleset));
				FileStream file = File.Create(path);

				write.Serialize(file, item);
				file.Close();
			}
			catch
			{
				throw;
			}
		}

		// Reads information in a given XML file defined by path and turns it into a LifeRuleset
		// Can throw typical FileStream creation or read errors
		public static T ReadObjectFromXMLFile<T>(string path) where T : class
		{
			try
			{
				T item;
				XmlSerializer read = new XmlSerializer(typeof(T));
				StreamReader reader = new StreamReader(path);
				item = (T)read.Deserialize(reader);

				return item;
			}
			catch
			{
				throw;
			}
		}
	}
}
