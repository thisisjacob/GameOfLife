using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private GameState mainGame; // Object for holding the current status of the game
        private bool isPlaying = false;
        public MainWindow()
        {
            InitializeComponent();
            LifeBoard.Loaded += InitializeProgram; // initializes board
        }

        // When fired, erases all the current graphics on the LifeBoard canvas and creates the graphics for the game
        void InitializeProgram(object sender, EventArgs e)
        {
            mainGame = new GameState(32, (int)LifeBoard.ActualWidth, (int)LifeBoard.ActualHeight);
            DrawingHelper.DrawGameBoard(LifeBoard, mainGame);
        }

        // When fired, calculate the next turn, redraw the canvas LifeBoard with the updated state
        void AdvanceStep(object sender, RoutedEventArgs e)
        {
            mainGame.GameStep();
            DrawingHelper.RedrawGameBoard(LifeBoard, mainGame);
        }

        // When fired, move mainGame back one step in history, redraw game board
        void ReverseStep(object sender, RoutedEventArgs e)
        {
            mainGame.GoBackStep();
            DrawingHelper.RedrawGameBoard(LifeBoard, mainGame);
        }

        // When fired, finds the position of the cursor, redraws the canvas and then highlights the cell the cursor is over
        void CanvasMouseMovement(object sender, RoutedEventArgs e)
        {
            Point position = Mouse.GetPosition(sender as Canvas);
            DrawingHelper.RedrawGameBoard(LifeBoard, mainGame);
            DrawingHelper.DrawHighlightedCell(LifeBoard, (int)position.X, (int)position.Y, mainGame);
        }

        // When fired, finds the position of the cursor, flips the status of the cell (dead/alive or false/true) and then redraws the canvas
        public void CanvasCellClicked(object sender, RoutedEventArgs e)
        {
            Point position = Mouse.GetPosition(LifeBoard);
            mainGame.SwitchStatus((int)position.X, (int)position.Y);
            DrawingHelper.RedrawGameBoard(LifeBoard, mainGame);
        }

        // does nothing meaningful for now
        public async void Play(object sender, RoutedEventArgs e)
        {
            isPlaying = true;
        }

        // does nothing meaningful for now
        public void Stop(object sender, RoutedEventArgs e)
        {
            isPlaying = false;
        }

    }
}
