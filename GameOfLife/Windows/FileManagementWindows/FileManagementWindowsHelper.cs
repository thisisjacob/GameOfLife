using System.IO;
using System.Runtime.Serialization;

namespace GameOfLife.Windows.FileManagementWindows
{
	public static class FileManagementWindowsHelper
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

		// Returns true if the passed string for a file name does not exist in the current directory
		// Returns false if this is not the case
		public static bool IsSaveDirectoryUnique(string fileName)
		{
			string dir = Directory.GetCurrentDirectory();
			dir += "\\" + fileName;
			return !Directory.Exists(dir);
		}
	}
}
