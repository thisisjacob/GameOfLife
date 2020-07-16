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
		List<ISerializable> Items;
		public SaveFileWindow()
		{
			InitializeComponent();
			Items = new List<ISerializable>();
		}

		// Adds an item you wish to save to the save window. Must be called with at least one item to actually save anything to file
		public void AddSavableItem<T>(T item) where T : ISerializable, new()
		{
			Items.Add(item);
		}

		// If valid, save and close program
		// If invalid (file already exists in directory), then notify user
		void Submit(object sender, RoutedEventArgs e)
		{
			if (FileManagementWindowsHelper.IsSaveDirectoryUnique(FileName.Text))
			{
				foreach(ISerializable item in Items)
				{
					// TODO: check writer
					FileManagement.FileManagement.WriteGameStatusObjectToFile(FileName.Text, item);
				}
				Close();

			}
			// alternative for creating notification of preexisting file
		}
	}
}
