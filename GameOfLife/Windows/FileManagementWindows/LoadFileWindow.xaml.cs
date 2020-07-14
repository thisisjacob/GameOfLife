using System.Windows;
using System.Windows.Controls;
using GameOfLife.GameStatus.LifeRulesetFiles;

namespace GameOfLife.Windows.FileManagementWindows
{
	/// <summary>
	/// Interaction logic for LoadFile.xaml
	/// </summary>
	public partial class LoadFileWindow : Window
	{
		LifeRuleset BackupRuleset;
		LifeRuleset SelectedRuleset;
		bool IsSubmitted;

		public LoadFileWindow(LifeRuleset backup)
		{
			InitializeComponent();
			Files.ItemsSource = FileManagementWindowsHelper.XMLFiles();
			BackupRuleset = backup;
			IsSubmitted = false;
		}

		// sets BackupRuleset to be the LifeRuleset currently selected in the Files ListBox
		void NewSelectedItem(object sender, RoutedEventArgs e)
		{
			ListBox list = (ListBox)sender;
			if (BackupRuleset != null)
			{
				LifeRulesetSerializer toSerialize = new LifeRulesetSerializer(BackupRuleset);
				SelectedRuleset = FileManagementWindowsHelper.SelectLifeRulesetFromFileItem((string)list.SelectedItem, toSerialize).ConvertToLifeRuleset();
			}
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
