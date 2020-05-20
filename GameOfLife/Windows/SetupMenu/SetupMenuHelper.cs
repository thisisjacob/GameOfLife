using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace GameOfLife.Windows.SetupMenu
{
	public class SetupMenuHelper
	{
		List<int> SelectedLivingNumbers;
		List<int> SelectedGrowthNumbers;
		List<int> SelectedDyingNumbers;

		public SetupMenuHelper()
		{
			SelectedLivingNumbers = new List<int>();
			SelectedGrowthNumbers = new List<int>();
			SelectedDyingNumbers = new List<int>();
		}
		public void ChangeRuleset(Window setupMenu, Button selectedButton)
		{

		}
	}
}
