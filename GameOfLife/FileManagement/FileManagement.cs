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

		public static object GetGameStatusObjectFromFile(string path, object itemObject)
		{
			string newPath = PathWithObjectTypeAppended(path, itemObject);
			try
			{
				if (itemObject.GetType() == typeof(LifeRuleset))
				{
					return FileReadWrite.ReadObjectFromXMLFile(newPath, (LifeRuleset)itemObject);
				}
				else if (itemObject.GetType() == typeof(GameState))
				{
					return FileReadWrite.ReadObjectFromXMLFile(newPath, (GameState)itemObject);
				}
				// if it is not one of the valid types, then the argument is invalid and it throws an exception
				else
				{
					throw new ArgumentException();
				}
			}
			catch (Exception e)
			{
				ErrorCatcher(e);
				return itemObject;
			}
		}

		// Returns path, altered so that it has the full type name of itemobject at the end, with a .xml extension
		static string PathWithObjectTypeAppended(string path, object itemObject) {
			return Path.GetFileNameWithoutExtension(path) + itemObject.GetType().ToString() + ".xml";
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
