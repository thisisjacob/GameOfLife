using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
	}
}
