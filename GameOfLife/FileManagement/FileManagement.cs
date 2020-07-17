// This is the main class for interacting wi

using System;
using System.IO;
using System.Runtime.Serialization;
using GameOfLife.Windows;

namespace GameOfLife.FileManagement
{
	static class FileManagement
	{
		// Returns an object from an XML file in the same directory as the executable, assuming that the file is accessible and the given itemObject implements
		// ISerializable and has a parameterless constructor
		// path is the path to read from, including extension name, itemObject is a backup object that is returned if reading fails
		public static T GetGameStatusObjectFromFile<T>(string path, T itemObject) where T : ISerializable, new()
		{
			try
			{
				return FileReadWrite.ReadObjectFromXMLFile<T>(path);
			}
			catch (Exception e)
			{
				ErrorCatcher(e);
				return itemObject;
			}
		}

		// Writes itemObject to an XML file as specified by path
		// Must implement ISerializable, and have a parameterless constructor
		public static void WriteGameStatusObjectToFile<T>(string path, T itemObject) where T : ISerializable
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
		
		// Accepts an Exception e, and opens a new NotificationWindow window showing a message for that exception
		// Assuming it is an UnauthorizedAccessException, ArgumentException, ArgumentNullException, DirectoryNotFoundException, or an IOException
		// If it is not one of these, then the caught error is thrown because that means it cannot be user or computer error
		static void ErrorCatcher(Exception e)
		{
			NotificationWindow window;
			if (e.GetType() == typeof(UnauthorizedAccessException))
			{
				window = new NotificationWindow("You do not have access to this file.");
				window.ShowDialog();
			}
			else if (e.GetType() == typeof(ArgumentException) || e.GetType() == typeof(ArgumentNullException))
			{
				window = new NotificationWindow("The specified path is empty.");
				window.ShowDialog();
			}
			else if (e.GetType() == typeof(DirectoryNotFoundException))
			{
				window = new NotificationWindow("This item was not found.");
				window.ShowDialog();
			}
			else if (e.GetType() == typeof(IOException))
			{
				window = new NotificationWindow("There was an issue in managing the file.");
				window.ShowDialog();
			}
			else // if it reaches this point, then it is a developer error and signals that something is wrong with the program itself
			{
				throw e;
			}


		}

	}
}
