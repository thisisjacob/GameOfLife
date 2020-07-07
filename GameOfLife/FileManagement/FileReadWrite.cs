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


		public static void WriteLifeRulesetToFile(LifeRuleset rules, string path)
		{
			LifeRuleset item = new LifeRuleset();
			item.InitializeForSerialization(rules.GetGrowthArray(), rules.GetLivingArray(), rules.GetDeathArray());
			XmlSerializer write = new XmlSerializer(typeof(LifeRuleset));
			FileStream file = File.Create(path);

			write.Serialize(file, item);
			file.Close();
		}

		public static LifeRuleset ReadLifeRulesetFromFile(string path)
		{
			LifeRuleset item;
			XmlSerializer read = new XmlSerializer(typeof(LifeRuleset));
			StreamReader reader = new StreamReader(path);
			item = (LifeRuleset)read.Deserialize(reader);

			return item;
		}



	}
}
