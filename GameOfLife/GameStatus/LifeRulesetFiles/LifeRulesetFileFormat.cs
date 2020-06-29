using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.GameStatus.LifeRulesetFiles
{
	// DATA FORMAT
	// The data is read as a series of strings, with a total length of 13 lines
	// It begins with the char 'G' (GrowthArray), followed by a total of 0-9 numbers in the range of 0-8
	// Then it is followed by the char 'L' (LivingArray), following the same number rules
	// Then 'D' (DyingArray), same rule
	// Then 'E' to denote the end of file
	// The numbers are not allowed to repeat
	public class LifeRulesetFileFormat
	{
		// defined number of lines there must be to be valid in this format
		// it is the total possible number of unique digits (0-8) + number of needed char tags ('G', 'L', 'D', 'E') (4) 
		const int NUMBER_OF_LINES = 13;

		// true as long as conversion to or from file is valid
		bool IsValid; 
		readonly List<int> GrowthList;
		readonly List<int> LivingList;
		readonly List<int> DyingList;
		List<string> Data;

		// Converts a List of strings representing lines from a file
		// Should be 13 characters in length, beginning with a G followed by 0-9 digits, then an L followed by 0-9 digits, then a D, followed by 0-9 digits, then an E
		// With each digit in the list being entirely unique
		// if fileLines can be converted into a valid LifeRuleset, then GetIsValid() will return true after initialization
		public LifeRulesetFileFormat(List<string> fileLines)
		{
		}

		// Converts a preexisting LifeRuleset and its fields into a List<string> that can easily be written to file
		public LifeRulesetFileFormat(LifeRuleset rulesetToWrite)
		{
			GrowthList = new List<int>(rulesetToWrite.GetGrowthArray());
			LivingList = new List<int>(rulesetToWrite.GetLivingArray());
			DyingList = new List<int>(rulesetToWrite.GetDeathArray());
			Data = FormatToFile();
		}

		// returns the data in this object as a List<string>, IsValid set to true if successful
		// where each line is either "G", "L", "D", each denoting the start of information for growing, living or dying arrays
		// , or a string of an int associated with that array, or E for the end of the data
		public List<string> FormatToFile()
		{
			List<string> data = new List<string>();

			data.Add("G");
			foreach (int num in GrowthList)
			{
				data.Add(num.ToString());
			}
			data.Add("L");
			foreach (int num in LivingList)
			{
				data.Add(num.ToString());
			}
			data.Add("D");
			foreach (int num in DyingList)
			{
				data.Add(num.ToString());
			}
			data.Add("E");

			IsValid = ValidateFileFormat(data);

			return data;
		}


		public bool ValidateFileFormat(List<string> data)
		{
			if (data.Count != NUMBER_OF_LINES)
			{
				return false;
			}
			int i = 0;
			if (data[i] != "G")
			{
				return false;
			}
			else
			{
				while (data[i] != "L")
				{
					i++;
				}
			}
			if (data[i] != "")
			{

			}

			return false;
		}
	}
}
