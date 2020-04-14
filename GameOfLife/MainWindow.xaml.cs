using System;
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
        private readonly int CANVAS_LENGTH;
        private const int CELL_LENGTH_NUM = 16;
        private readonly int CELL_LENGTH_PIXEL;
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
            InitializeComponent();
            CANVAS_LENGTH = (int)LifeBoard.Width;
            CELL_LENGTH_PIXEL = CANVAS_LENGTH / CELL_LENGTH_NUM;
            GameLogic.GameStep(lifeCells); // TEST STEPS
            DrawingHelper.TestDraw(LifeBoard, CELL_LENGTH_PIXEL, CELL_LENGTH_PIXEL, 0, 0);
        }
    }
}
