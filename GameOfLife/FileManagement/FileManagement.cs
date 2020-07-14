// This is the main class for interacting wi

using System;
using System.IO;
using System.Runtime.Serialization;
using GameOfLife.Windows.NotificationWindows;

namespace GameOfLife.FileManagement
{
	static class FileManagement
	{
		// Returns an object from an XML file in the same directory as the executable, assuming that the file is accessible and the given itemObject implements
		// ISerializable and has a parameterless constructor
		// path is the path to read from, itemObject is a backup object that is returned if reading fails
		public static T GetGameStatusObjectFromFile<T>(string path, T itemObject) where T : ISerializable, new()
		{
			try
			{
				if (itemObject.GetType() == typeof(LifeRuleset))
				{
					return FileReadWrite.ReadObjectFromXMLFile<T>(path);
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
		// Must implement ISerializable, and have a parameterless constructor
		public static void WriteGameStatusObjectToFile<T>(string path, T itemObject) where T : ISerializable, new()
		{
			try
			{
				FileReadWrite.WriteLifeSerializableToFile(itemObject, path);

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
