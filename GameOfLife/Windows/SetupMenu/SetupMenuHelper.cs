using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace GameOfLife.Windows.SetupMenu
{
	public class SetupMenuHelper
	{
		public List<int> SelectedLivingNumbers;
		public List<int> SelectedGrowthNumbers;
		public List<int> SelectedDyingNumbers;
		readonly Brush SelectedColor = Brushes.Green;
		readonly Brush NotSelectedColor = Brushes.Red;

		public SetupMenuHelper(LifeRuleset rulesetToModify)
		{
			SelectedLivingNumbers = new List<int>(rulesetToModify.GetLivingArray());
			SelectedGrowthNumbers = new List<int>(rulesetToModify.GetGrowthArray());
			SelectedDyingNumbers = new List<int>(rulesetToModify.GetDeathArray());
		}

		// Updates the three selectedXSTATUSNumbers Lists
		// Ensures that there are no repeated digits between lists
		public void ChangeRuleset(ListBox modifiedList, int numberToChange)
		{
			SelectedLivingNumbers.Remove(numberToChange);
			SelectedGrowthNumbers.Remove(numberToChange);
			SelectedDyingNumbers.Remove(numberToChange);

			if (modifiedList.Name.Equals("LivingList"))
				SelectedLivingNumbers.Add(numberToChange);
			else if (modifiedList.Name.Equals("GrowingList"))
				SelectedGrowthNumbers.Add(numberToChange);
			else if (modifiedList.Name.Equals("DyingList"))
				SelectedDyingNumbers.Add(numberToChange);
			else
				throw new ArgumentException("Unrecognized ListBox");
		}

		// Recolors the three given ListBox subitems based on the Brush colors defined at the header
		public void RecolorBoxes(ListBox living, ListBox growing, ListBox dying)
		{
			if (living.Items.Count != 9 || growing.Items.Count != 9 || dying.Items.Count != 9)
				throw new ArgumentException("One of the given ListBox does not have 9 items");
			for (int i = 0; i < 9; i++)
			{
				if (SelectedLivingNumbers.Contains(i))
					((ListBoxItem)living.Items[i]).Background = SelectedColor;
				else
					((ListBoxItem)living.Items[i]).Background = NotSelectedColor;

				if (SelectedGrowthNumbers.Contains(i))
					((ListBoxItem)growing.Items[i]).Background = SelectedColor;
				else
					((ListBoxItem)growing.Items[i]).Background = NotSelectedColor;

				if (SelectedDyingNumbers.Contains(i))
					((ListBoxItem)dying.Items[i]).Background = SelectedColor;
				else
					((ListBoxItem)dying.Items[i]).Background = NotSelectedColor;
			}
		}
	}
}
