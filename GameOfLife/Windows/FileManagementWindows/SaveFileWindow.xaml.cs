using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Documents;

namespace GameOfLife.Windows.FileManagementWindows
{
	// User should use the constructor, then add any classes that they wish to save using AddSavableItem
	// The argument passed to AddSavableItem must implement ISerializable, and have a no-argument constructor
	// Because it uses XML serialization for saving
	public partial class SaveFileWindow : Window
	{
		ISerializable Item;
		public SaveFileWindow()
		{
			InitializeComponent();
		}

		// Adds an item you wish to save to the save window. Must be called with at least one item to actually save anything to file
		public void AddSavableItem<T>(T item) where T : ISerializable, new()
		{
			Item = item;
		}

		// If valid, save and close program
		// If invalid (file already exists in directory), then notify user
		void Submit(object sender, RoutedEventArgs e)
		{
			if (FileManagementWindowsHelper.IsSaveDirectoryUnique(FileName.Text) && Item != null)
			{
				FileManagement.FileManagement.WriteGameStatusObjectToFile(FileName.Text, Item);
				Close();
			}
			// because we want to do nothing if Item is null
			// informs the user if the file already exists
			else if (Item != null)
			{
				NotificationWindow window = new NotificationWindow("A file with this name already exists.");
				window.ShowDialog();
			}
		}
	}
}
