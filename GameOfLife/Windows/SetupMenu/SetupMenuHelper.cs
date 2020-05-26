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
		public List<int> SelectedLivingNumbers;
		public List<int> SelectedGrowthNumbers;
		public List<int> SelectedDyingNumbers;
		Brush SelectedColor = Brushes.Green;
		Brush NotselectedColor = Brushes.Blue;

		public SetupMenuHelper()
		{
			SelectedLivingNumbers = new List<int>();
			SelectedGrowthNumbers = new List<int>();
			SelectedDyingNumbers = new List<int>();
		}

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

		public void RecolorBoxes(ListBox living, ListBox growing, ListBox dying)
		{
			if (living.Items.Count != 10 || growing.Items.Count != 10 || dying.Items.Count != 10)
				throw new ArgumentException("One of the given ListBox does not have 10 items");
			for (int i = 0; i < 10; i++)
			{
				if (SelectedLivingNumbers.Contains(i))
					((ListBoxItem)living.Items[i]).Background = SelectedColor;
				else
					((ListBoxItem)living.Items[i]).Background = NotselectedColor;

				if (SelectedGrowthNumbers.Contains(i))
					((ListBoxItem)growing.Items[i]).Background = SelectedColor;
				else
					((ListBoxItem)growing.Items[i]).Background = NotselectedColor;

				if (SelectedDyingNumbers.Contains(i))
					((ListBoxItem)dying.Items[i]).Background = SelectedColor;
				else
					((ListBoxItem)dying.Items[i]).Background = NotselectedColor;
			}

		}
	}
}
