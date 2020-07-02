using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using GameOfLife.Windows.NotificationWindows;
namespace GameOfLife.GameStatus.LifeRulesetFiles
{
	// DATA FORMAT
	// The data is read as a series of strings, with a total length of 13 lines
	// It begins with the char 'G' (GrowthArray), followed by a total of 0-9 numbers in the range of 0-8
	// Then it is followed by the string 'L' (LivingArray), following the same number rules
	// Then 'D' (DyingArray), same rule
	// Then 'E' to denote the end of file
	// The numbers are not allowed to repeat
	public class LifeRulesetFileFormat
	{
		// CONSTANTS:
		// defined number of lines there must be to be valid in this format
		// it is the total possible number of unique digits in (0-8) + number of needed string tags ("G", "L", "D", "E") (4) 
		const int NUMBER_OF_LINES = 13;
		const string GROWTH_START = "G";
		const string LIVING_START = "L";
		const string DYING_START = "D";
		const string END = "E";
		static Regex ORDER_CHECK_REGEX = new Regex(@"G(\d*)L(\d*)D(\d*)E");

		// these will be modified during program runntime, rather than being treated as constants
		readonly List<int> GrowthList;
		readonly List<int> LivingList;
		readonly List<int> DyingList;
		// if there is a problem converting a LifeRuleset to file, return this to restore original LifeRuleset
		readonly LifeRuleset RulesetToReturn;

		// Converts a List of strings representing lines from a file
		// Should be 13 characters in length, beginning with a G followed by 0-9 digits, then an L followed by 0-9 digits, then a D, followed by 0-9 digits, then an E
		// With each digit in the list being entirely unique
		// if fileLines can be converted into a valid LifeRuleset, then GetIsValid() will return true after initialization
		// currentRuleset is the LifeRuleset currently in use by the main program. It is returned by ReturnLifeRuleset if loading data fails
		public LifeRulesetFileFormat(LifeRuleset currentRuleset, List<string> fileLines)
		{
			GrowthList = new List<int>();
			LivingList = new List<int>();
			DyingList = new List<int>();
			try
			{
				if (!ValidateFileFormat(fileLines))
					throw new FileFormatException();
				CreateRuleset(fileLines);
				RulesetToReturn = new LifeRuleset(GrowthList.ToArray(), LivingList.ToArray(), DyingList.ToArray());

			}
			catch (FileFormatException)
			{
				FileError window = new FileError("The provided data is invalid.");
				window.ShowDialog();
				RulesetToReturn = currentRuleset;
			}
		}

		// Converts a preexisting LifeRuleset and its fields into a List<string> that can easily be written to file
		// Should be called when you want to write to file
		public LifeRulesetFileFormat(LifeRuleset rulesetToWrite)
		{
			GrowthList = new List<int>(rulesetToWrite.GetGrowthArray());
			LivingList = new List<int>(rulesetToWrite.GetLivingArray());
			DyingList = new List<int>(rulesetToWrite.GetDeathArray());
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

			return data;
		}

		// Returns true if the given list conforms to the Data Format specification for LifeRuleset at the top of this file
		// Returns false if it does not conform
		public static bool ValidateFileFormat(List<string> data)
		{
			HashSet<int> numbers = new HashSet<int>();
			if (data.Count != NUMBER_OF_LINES)
				return false;
			// check order according to format specification
			else if (ORDER_CHECK_REGEX.IsMatch(data.ToString()))
				return false;
			foreach (string item in data)
			{
				if (Char.IsDigit(Convert.ToChar(item)))
				{
					// no exception check required because the this only fires if item is a digit
					numbers.Add(Int32.Parse(item));
				}
			}
			// no check for repeated numbers is needed because the check for both count and no missing digits prevents the possibility of repeats being accepted

			// if there are mission digits, return false. otherwise, since all other checks are complete, return true
			return !LifeRuleset.AreThereAnyMissingDigitsSingleSet(numbers);
		}

		// Assuming that the provided data from a file or user provided List<string> has been successfully validated, this will add the numbers in data to their appropriate rulesets
		// This LifeRulesetFileFormat will then be ready to be converted to file
		void CreateRuleset(List<string> data)
		{
			// checks if the current instance of LifeRulesetFileFormat already has a defined ruleset, ends function call if so
			if (GrowthList.Count != 0 || LivingList.Count != 0 || DyingList.Count != 0)
				return;

			// updates lastRuleSection with the Growing/Living/Dying tag of the current region of data
			// adds index that is currently being parsed to appropriate List for LifeRuleset rules
			string lastRuleSection = "";
			foreach (string item in data)
			{
				if (!Char.IsDigit(Convert.ToChar(item)))
					lastRuleSection = item;
				else if (lastRuleSection.Equals(GROWTH_START))
					GrowthList.Add(Int32.Parse(item));
				else if (lastRuleSection.Equals(LIVING_START))
					LivingList.Add(Int32.Parse(item));
				else if (lastRuleSection.Equals(DYING_START))
					DyingList.Add(Int32.Parse(item));
			}
		}

		// Returns a LifeRuleset created by data that was provided in the constructor, assuming it was found to be valid using the data format specification
		// Otherwise, returns the LifeRuleset that was provided in the constructor
		public LifeRuleset ReturnLifeRuleset()
		{
			return RulesetToReturn;
		}
	}
}
