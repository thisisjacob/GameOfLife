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

namespace GameOfLife.Windows.FileManagementWindows
{
	/// <summary>
	/// Interaction logic for LoadFile.xaml
	/// </summary>
	public partial class LoadFile : Window
	{
		LifeRuleset BackupRuleset;
		LifeRuleset SelectedRuleset;

		public LoadFile(LifeRuleset backup)
		{
			InitializeComponent();
			Files.ItemsSource = FileManagementHelper.XMLFiles();
			BackupRuleset = backup;
		}

		// sets BackupRuleset to be the LifeRuleset currently selected in the Files ListBox
		void NewSelectedItem(object sender, RoutedEventArgs e)
		{
			ListBox list = (ListBox)sender;
			SelectedRuleset = FileManagementHelper.SelectLifeRulesetFromFileItem((string)list.SelectedItem, BackupRuleset);
		}
	}
}
