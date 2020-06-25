using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace GameOfLife.FileManagement
{
	public class GameStateFile
	{
		GameState gameInfo { get; set; }
		LifeRuleset rulesInfo { get; set; }
		string fileName { get; set; }

		// Only valid constructor. For saving or reading from file
		GameStateFile(GameState game, LifeRuleset rules, string file)
		{
			gameInfo = game;
			rulesInfo = rules;
			fileName = file;
		}
	}
}
