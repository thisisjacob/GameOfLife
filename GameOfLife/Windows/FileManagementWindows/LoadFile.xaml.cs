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
		bool IsSubmitted;

		public LoadFile(LifeRuleset backup)
		{
			InitializeComponent();
			Files.ItemsSource = FileManagementHelper.XMLFiles();
			BackupRuleset = backup;
			IsSubmitted = false;
		}

		// sets BackupRuleset to be the LifeRuleset currently selected in the Files ListBox
		void NewSelectedItem(object sender, RoutedEventArgs e)
		{
			ListBox list = (ListBox)sender;
			SelectedRuleset = FileManagementHelper.SelectLifeRulesetFromFileItem((string)list.SelectedItem, BackupRuleset);
		}

		void Submitted(object sender, RoutedEventArgs e)
		{
			IsSubmitted = true;
			Close();
		}

		// For getting the result of user choices from LoadFile
		public LifeRuleset ReturnResult()
		{
			if (IsSubmitted && SelectedRuleset != null)
				return SelectedRuleset;
			else
				return BackupRuleset;
		}
	}
}
