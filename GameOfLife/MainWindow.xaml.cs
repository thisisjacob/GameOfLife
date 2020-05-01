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
        private const int CELL_LENGTH_NUM = 32;
        private bool[,] lifeCells = new bool[CELL_LENGTH_NUM, CELL_LENGTH_NUM];
        public MainWindow()
        {
            InitializeComponent();
            LifeBoard.Loaded += InitializeLifeBoardGraphics; // initializes board
            
        }

        // When fired, erases all the current graphics on the LifeBoard canvas and creates the graphics for the game
        private void InitializeLifeBoardGraphics(object sender, EventArgs e)
        {
            DrawingHelper.DrawGameBoard(LifeBoard, lifeCells, CELL_LENGTH_NUM);
        }

        // When fired, calculate the next turn, redraw the canvas LifeBoard with the updated state
        private void AdvanceStep(object sender, RoutedEventArgs e)
        {
            lifeCells = GameLogic.GameStep(lifeCells);
            DrawingHelper.RedrawGameBoard(LifeBoard, lifeCells, CELL_LENGTH_NUM);
        }

        // When fired, finds the position of the cursor, redraws the canvas and then highlights the cell the cursor is over
        private void CanvasMouseMovement(object sender, RoutedEventArgs e)
        {
            Point position = Mouse.GetPosition(sender as Canvas);
            DrawingHelper.RedrawGameBoard(LifeBoard, lifeCells, CELL_LENGTH_NUM);
            DrawingHelper.DrawHighlightedCell(LifeBoard, (int)position.X, (int)position.Y, CELL_LENGTH_NUM);
        }

        // When fired, finds the position of the cursor, flips the status of the cell (dead/alive or false/true) and then redraws the canvas
        public void CanvasCellClicked(object sender, RoutedEventArgs e)
        {
            Point position = Mouse.GetPosition(LifeBoard);
            lifeCells = GameLogic.SwitchStatus(LifeBoard, lifeCells, (int)position.X, (int)position.Y, (int) LifeBoard.ActualWidth / CELL_LENGTH_NUM);
            DrawingHelper.RedrawGameBoard(LifeBoard, lifeCells, CELL_LENGTH_NUM);
        }
    }
}
