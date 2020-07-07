// This is the main class for interacting wi

using System;
using System.Collections.Generic;
using System.Text;
using GameOfLife.GameStatus.LifeRulesetFiles;

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
		public static LifeRuleset ReadLifeRulesetFromFile(LifeRuleset rulesetBackup, string path)
		{
			List<string> items = FileReadWrite.ReadFromFile(path);
			LifeRulesetFileFormat file = new LifeRulesetFileFormat(rulesetBackup, items);
			return file.ReturnLifeRuleset();
		}

	}
}
