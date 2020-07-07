﻿using System;
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

namespace GameOfLife.Windows.NotificationWindows
{
	/// <summary>
	/// Interaction logic for FileError.xaml
	/// </summary>
	/// 

	//TODO: make errorInformation set text of main TextBlock


	public partial class FileError : Window
	{
		public FileError(string errorInformation)
		{
			InitializeComponent();
			CloseButton.Click += (sender, args) => this.Close();
			ErrorBlock.Text = errorInformation;
		}
	}
}
