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

		void RulesetSelectorButton(object args, RoutedEventArgs e)
		{

		}
	}
}
