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
