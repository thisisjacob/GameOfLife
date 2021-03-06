﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GameOfLife.Windows.SetupMenu;

namespace GameOfLife
{
	// A window allowing the user to create a new LifeRuleset with their own supplied rules, 
	// as well as changing the size of the game board
	public partial class SetupMenu : Window
	{
		// Holds LifeRuleset updates, holds helper functions for this window
		SetupMenuHelper helper;
		int currentLength;
		string backupEnteredTextString;

		// Constants 
		const int MAXIMUMUM_LENGTH = 100;

		public SetupMenu(LifeRuleset rulesetToModify, GameState currentGame)
		{
			InitializeComponent();
			helper = new SetupMenuHelper(rulesetToModify);
			helper.RecolorBoxes(LivingList, GrowingList, DyingList);
			backupEnteredTextString = "";
			currentLength = currentGame.Length();
			EnteredLength.Text = currentLength.ToString();
		}

		// Converts objects and routes them to their functions
		// Used to make reading of the route functions easier
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

		// For use outside of the SetupMenu. For converting helper into a LifeRuleset for the program, must always be called to set the LifeRuleset of the main program
		public LifeRuleset NewRuleset()
		{
			return new LifeRuleset(helper.SelectedGrowthNumbers.ToArray(), helper.SelectedLivingNumbers.ToArray(), helper.SelectedDyingNumbers.ToArray());
		}

		// Returns the new Length of the board. Should be given to a new GameState object, and the game board should be redrawn
		public int NewGameStateLength()
		{
			return currentLength;
		}

		void CloseButton(object e, RoutedEventArgs eventArgs)
		{
			Close();
		}

		// Ensures only numbers can be entered into EnteredLength.Text
		void EnteredLength_KeyDown(object sender, KeyEventArgs e)
		{
			// if key is not a number, set handled to true, prevents non digit input from being routed further
			e.Handled = e.Key < Key.D0 || e.Key > Key.D9 || Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift); ;
		}

		// Updates the recorded currentLength
		// Enforces maximum Length limit (will not allow a number higher than the limit)
		void UpdateEnteredLength(object sender, TextChangedEventArgs e)
		{
			if (EnteredLength.Text.Length != 0)
				currentLength = int.Parse(EnteredLength.Text);

			if (currentLength > MAXIMUMUM_LENGTH)
			{
				EnteredLength.Text = backupEnteredTextString;
				currentLength = int.Parse(EnteredLength.Text);
			}
			backupEnteredTextString = EnteredLength.Text;
		}
	}
}
