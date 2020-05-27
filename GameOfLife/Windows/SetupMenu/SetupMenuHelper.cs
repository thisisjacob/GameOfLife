using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GameOfLife.Windows.SetupMenu
{
	public class SetupMenuHelper
	{
		public List<int> selectedLivingNumbers;
		public List<int> selectedGrowthNumbers;
		public List<int> selectedDyingNumbers;
		Brush selectedColor = Brushes.Green;
		Brush notSelectedColor = Brushes.Blue;

		public SetupMenuHelper(LifeRuleset rulesetToModify)
		{
			selectedLivingNumbers = new List<int>(rulesetToModify.GetLivingArray());
			selectedGrowthNumbers = new List<int>(rulesetToModify.GetGrowthArray());
			selectedDyingNumbers = new List<int>(rulesetToModify.GetDeathArray());
		}

		// Updates the three selectedXSTATUSNumbers Lists
		// Ensures that there are no repeated digits between lists
		public void ChangeRuleset(ListBox modifiedList, int numberToChange)
		{
			selectedLivingNumbers.Remove(numberToChange);
			selectedGrowthNumbers.Remove(numberToChange);
			selectedDyingNumbers.Remove(numberToChange);

			if (modifiedList.Name.Equals("LivingList"))
				selectedLivingNumbers.Add(numberToChange);
			else if (modifiedList.Name.Equals("GrowingList"))
				selectedGrowthNumbers.Add(numberToChange);
			else if (modifiedList.Name.Equals("DyingList"))
				selectedDyingNumbers.Add(numberToChange);
			else
				throw new ArgumentException("Unrecognized ListBox");
		}

		// Recolors the three given ListBox subitems based on the Brush colors defined at the header
		public void RecolorBoxes(ListBox living, ListBox growing, ListBox dying)
		{
			if (living.Items.Count != 10 || growing.Items.Count != 10 || dying.Items.Count != 10)
				throw new ArgumentException("One of the given ListBox does not have 10 items");
			for (int i = 0; i < 10; i++)
			{
				if (selectedLivingNumbers.Contains(i))
					((ListBoxItem)living.Items[i]).Background = selectedColor;
				else
					((ListBoxItem)living.Items[i]).Background = notSelectedColor;

				if (selectedGrowthNumbers.Contains(i))
					((ListBoxItem)growing.Items[i]).Background = selectedColor;
				else
					((ListBoxItem)growing.Items[i]).Background = notSelectedColor;

				if (selectedDyingNumbers.Contains(i))
					((ListBoxItem)dying.Items[i]).Background = selectedColor;
				else
					((ListBoxItem)dying.Items[i]).Background = notSelectedColor;
			}
		}
	}
}
