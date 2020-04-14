﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        private const int CELL_LENGTH_NUM = 16;
        private bool[,] lifeCells = new bool[CELL_LENGTH_NUM, CELL_LENGTH_NUM];
        public MainWindow()
        {
            // TESTING R-PENTOMINO
            lifeCells[7, 8] = true;
            lifeCells[7, 9] = true;
            lifeCells[8, 9] = true;
            lifeCells[8, 10] = true;
            lifeCells[6, 9] = true;
            // TESTING

            DrawingHelper.DrawGameBoard(LifeBoard, lifeCells, CELL_LENGTH_NUM);
            //GameLogic.GameStep(lifeCells); // TEST STEPS
            //DrawingHelper.DrawCell(LifeBoard, CELL_LENGTH_PIXEL, CELL_LENGTH_PIXEL, 0, 0, false);
        }
    }
}
