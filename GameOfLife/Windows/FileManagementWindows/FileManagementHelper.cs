using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Controls;
using GameOfLife.GameStatus.LifeRulesetFiles;

namespace GameOfLife.Windows.FileManagementWindows
{
	public static class FileManagementHelper
	{
		const string XML_TAG = "*.xml";

		// Returns an array with a string array of the name of each XML file in the current directory
		// Extensions are stripped
		public static string[] XMLFiles()
		{
			string currentDir = Directory.GetCurrentDirectory();
			string[] items = Directory.GetFiles(currentDir, XML_TAG);
			
			for (int i = 0; i < items.Length; i++)
			{
				items[i] = Path.GetFileNameWithoutExtension(items[i]);
			}

			return items;
		}

		// Retrieves a LifeRuleset using the content of item, assuming item is just the name of the path to an XML file, without
		// its extension or full directory
		public static T SelectLifeRulesetFromFileItem<T>(string item, T previousSelected) where T : ISerializable, new()
		{
			string path = Directory.GetCurrentDirectory() + "\\" + item + ".xml";
			return FileManagement.FileManagement.GetGameStatusObjectFromFile(path, previousSelected);
		}
	}
}
