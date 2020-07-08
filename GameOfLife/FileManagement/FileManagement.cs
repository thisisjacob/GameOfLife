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
		// Returns an object from an XML file in the same directory as the executable, assuming that the file is accessible and the given itemObject is of
		// Type: LifeRuleset
		// path is the path to read from, itemObject is a backup object that is returned if reading fails
		// NOTE: Result must be typecasted to type of itemObject, only an object type is returned
		public static object GetGameStatusObjectFromFile(string path, object itemObject)
		{
			try
			{
				if (itemObject.GetType() == typeof(LifeRuleset))
				{
					return FileReadWrite.ReadObjectFromXMLFile(path, (LifeRuleset)itemObject);
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

		// Writes itemObject to an XML file as specified by path
		// Must be of 
		// Type: LifeRuleset
		public static void WriteGameStatusObjectToFile(string path, object itemObject)
		{
			try
			{
				if (itemObject.GetType() == typeof(LifeRuleset))
				{
					FileReadWrite.WriteLifeRulesetToXMLFile((LifeRuleset)itemObject, path);
				}
				else
				{
					throw new ArgumentException();
				}
			}
			catch (Exception e)
			{
				ErrorCatcher(e);
			}
		}
		
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
