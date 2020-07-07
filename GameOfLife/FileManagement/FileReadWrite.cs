using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace GameOfLife.FileManagement
{
	public static class FileReadWrite
	{
		// returns the lines in a file with directory path as a List<string>
		public static List<string> ReadFromFile(string path)
		{ 
			List<string> items = new List<string>();
			// adds each line to lifeRuleset
			using (StreamReader streamRead = new StreamReader(path))
			{
				string line;
				while ((line = streamRead.ReadLine()) != null)
				{
					items.Add(line);
				}
			}

			return items;
		}

		public static void WriteLifeRulesetToFile(List<string> lines)
		{

		}



	}
}
