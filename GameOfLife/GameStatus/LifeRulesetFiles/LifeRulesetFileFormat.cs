using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using GameOfLife.Windows.NotificationWindows;
// TODO: FINISH CREATING DATA VALIDATION FUNCTION
// TODO: CREATE FUNCTIONS TO RETURN DATA OR A LifeRuleset
// TODO: maybe use List<char> instead of List<string> for lines?
// TODO: TEST VALIDATION FUNCTION ONCE COMPLETE WITH DIFFERENT EXAMPLES OF NUMBER_OF_LINES LENGTH DATA
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
		// defined number of lines there must be to be valid in this format
		// it is the total possible number of unique digits (0-8) + number of needed string tags ("G", "L", "D", "E") (4) 
		const int NUMBER_OF_LINES = 13;
		const string GROWTH_START = "G";
		const string LIVING_START = "L";
		const string DYING_START = "D";
		const string END = "E";
		static Regex ORDER_CHECK_REGEX = new Regex(@"G(\d*)L(\d*)D(\d*)E");

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
			if (ValidateFileFormat(fileLines))
			{
				IsValid = true;
				// Call function for converting 
			}
			else
			{
				IsValid = false;
			}
		}

		// Converts a preexisting LifeRuleset and its fields into a List<string> that can easily be written to file
		// Should be called when you want to write to file
		public LifeRulesetFileFormat(LifeRuleset rulesetToWrite)
		{
			GrowthList = new List<int>(rulesetToWrite.GetGrowthArray());
			LivingList = new List<int>(rulesetToWrite.GetLivingArray());
			DyingList = new List<int>(rulesetToWrite.GetDeathArray());
			Data = FormatToFile();
		}

		
		// If data has been sucessfully read to this from a List<string> or a LifeRuleset has been passed as a constructor, then this returns a LifeRuleset if it creation was valid
		// Otherwise, it will create a FormatException to signify problems with format conversion
		public LifeRuleset CreateLifeRuleset()
		{
			if (IsValid)
			{
				return new LifeRuleset(GrowthList.ToArray(), LivingList.ToArray(), DyingList.ToArray());
			}
			else
			{
				throw new FormatException();
			}
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

			try
			{
				IsValid = ValidateFileFormat(data);
			}
			catch (FormatException) // TODO: should open a menu informing of problem with creating data
			{
				new FileError("DEVELOPER ERROR: FormatException calling ValidateFileFormat(List<string> data) in LifeRulesetFileFormat");
			}
			catch (ArgumentNullException) // TODO: should open a menu informing of problem with creating data
			{
				new FileError("DEVELOPER ERROR: ArgumentNullException calling ValidateFileFormat(List<string> data in LifeRulesetFileFormat)\nThere is an issue with provided data.");
			}

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

		// TODO: CREATE FUNCTION FOR CONVERTING DATA INTO A LIFE RULESET
	}
}
