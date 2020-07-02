using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace GameOfLife.FileManagement
{
	public class GameStateFile
	{
		GameState GameInfo { get; set; }
		LifeRuleset RulesInfo { get; set; }
		string FileName { get; set; }

		// Only valid constructor. For saving or reading from file
		GameStateFile(GameState game, LifeRuleset rules, string file)
		{
			GameInfo = game;
			RulesInfo = rules;
			FileName = file;
		}
	}
}
