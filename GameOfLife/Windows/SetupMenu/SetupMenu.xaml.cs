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
		SetupMenuHelper helper;
		public SetupMenu()
		{
			InitializeComponent();
			helper = new SetupMenuHelper();
		}

		void InteractionRouter(object e, RoutedEventArgs args)
		{
			if (e.GetType() == typeof(ListBox))
				RulesetSelectorButton((ListBox)e);
		}
		void RulesetSelectorButton(ListBox changedList)
		{
			var value = Int32.Parse((String)((ListBoxItem)changedList.SelectedItem).Tag);
			helper.ChangeRuleset(changedList, value);
			helper.RecolorBoxes(LivingList, GrowingList, DyingList);
		}
	}
}
