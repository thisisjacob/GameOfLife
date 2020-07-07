// This is the main class for interacting wi

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GameOfLife.Windows.NotificationWindows;

namespace GameOfLife.FileManagement
{
	static class FileManagement
	{
		// Attempts to read a LifeRuleset from the given path file
		// If an error is caught, ErrorCatcher is called to show the user information about the error
		// backup is then returned instead
		public static LifeRuleset GetRulesetFromFile(string path, LifeRuleset backup)
		{
			try
			{
				return FileReadWrite.ReadLifeRulesetFromXMLFile(path);
			}
			catch (Exception e)
			{
				ErrorCatcher(e);
				return backup;
			}
		}

		// TODO: create ruleset function
		
		// TODO: More ErrorCatcher possibilities
		static void ErrorCatcher(Exception e)
		{
			FileError window;
			if (e.GetType() == typeof(FileFormatException))
			{
				window = new FileError("The data format in the file is invalid.");
				window.ShowDialog();
			}
			
			
		}

	}
}
