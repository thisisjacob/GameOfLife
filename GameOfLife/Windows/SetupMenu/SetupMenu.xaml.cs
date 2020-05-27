using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GameOfLife.Windows.SetupMenu;

namespace GameOfLife
{
	/// <summary>
	/// Interaction logic for SetupMenu.xaml
	/// </summary>
	public partial class SetupMenu : Window
	{
		// Holds LifeRuleset updates, holds helper functions for this window
		SetupMenuHelper helper;
		int currentLength;

		public SetupMenu(LifeRuleset rulesetToModify, GameState currentGame)
		{
			InitializeComponent();
			helper = new SetupMenuHelper(rulesetToModify);
			helper.RecolorBoxes(LivingList, GrowingList, DyingList);
			currentLength = currentGame.Length();
			EnteredLength.Text = currentLength.ToString();
				
		}

		void InteractionRouter(object e, RoutedEventArgs args)
		{
			if (e.GetType() == typeof(ListBox))
				RulesetSelectorButton((ListBox)e);
		}

		// Fires whenever the ListBox selection in one of the three Living/Growing/Dying ListBoxes changes, called form InteractionRouter
		// Saves the new ruleset, recolors the boxes based on what is selected
		void RulesetSelectorButton(ListBox changedList)
		{
			// to prevent the following lines from firing immediately after SelectedItem of the list is set to null
			if (changedList.SelectedItem != null) 
			{
				var value = Int32.Parse((String)((ListBoxItem)changedList.SelectedItem).Tag);
				helper.ChangeRuleset(changedList, value);
				helper.RecolorBoxes(LivingList, GrowingList, DyingList);
				changedList.SelectedItem = null;
			}
		}

		// For use outside of the SetupMenu. For converting helper into a LifeRuleset for the program
		public LifeRuleset NewRuleset()
		{
			return new LifeRuleset(helper.selectedGrowthNumbers.ToArray(), helper.selectedLivingNumbers.ToArray(), helper.selectedDyingNumbers.ToArray());
		}

		void CloseButton(object e, RoutedEventArgs eventArgs)
		{
			Close();
		}

		private void EnteredLength_KeyDown(object sender, KeyEventArgs e)
		{
			// if key is not a number, set handled to true
			e.Handled = e.Key < Key.D0 || e.Key > Key.D9;
		}
	}
}
