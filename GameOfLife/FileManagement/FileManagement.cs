// This is the main class for interacting wi

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GameOfLife.GameStatus.LifeRulesetFiles;
using GameOfLife.Windows.NotificationWindows;

namespace GameOfLife.FileManagement
{
	static class FileManagement
	{
		// incomplete, does nothing meaningful at the moment
		public static void WriteLifeRulesetToFile(LifeRuleset rules, string path)
		{
			LifeRulesetFileFormat newFile = new LifeRulesetFileFormat(rules);
		}

		// TODO: setup ReadFromFile so there is a version just for LifeRulesets that reads first 13 files
		// TODO: remove backup system from LifeRulesetFileFormat, it is now unnecesarry
		// Reads from path to create a LifeRuleset, assuming the file can be accessed and it it meets the internal LifeRuleset rules
		// Otherwise, it just returns rulesetBackup
		public static LifeRuleset ReadLifeRulesetFromFile(LifeRuleset rulesetBackup, string path)
		{
			List<string> items;
			LifeRulesetFileFormat file;

			try
			{
				items = FileReadWrite.ReadFromFile(path);
				file = new LifeRulesetFileFormat(rulesetBackup, items);
			}
			catch (Exception e)
			{
				ErrorCatcher(e);
				return rulesetBackup;
			}

			return file.ReturnLifeRuleset();
		}

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
