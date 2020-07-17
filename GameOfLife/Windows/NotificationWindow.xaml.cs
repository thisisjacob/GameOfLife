using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GameOfLife.Windows
{
	/// <summary>
	/// Interaction logic for FileError.xaml
	/// </summary>
	/// 

	public partial class NotificationWindow : Window
	{
		// Opens the window with notificationInformation set as the text shown to the user.
		public NotificationWindow(string notificationInformation)
		{
			InitializeComponent();
			CloseButton.Click += (sender, args) => this.Close();
			ErrorBlock.Text = notificationInformation;
		}
	}
}
