using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        // Object for holding the current status of the game
        GameState mainGame; 
        // initialized with default Game of Life rules
        LifeRuleset rules = new LifeRuleset(new int[] { 3 }, new int[] { 2 }, new int[] {0, 1, 4, 5, 6, 7, 8, 9 });
        
        bool isPlaying = false;
        Timer playTimer;

        // Constants
        // Default length of LifeBoard
        const int DEFAULT_LENGTH = 32;
        // Default time of counter 
        const int COUNTER_TIME = 1000;

        public MainWindow()
        {
            InitializeComponent();
            LifeBoard.Loaded += InitializeProgram; // initializes board
        }

        // When fired, erases all the current graphics on the LifeBoard canvas and creates the graphics for the game
        void InitializeProgram(object sender, EventArgs e)
        {
            mainGame = new GameState(DEFAULT_LENGTH, (int)LifeBoard.ActualWidth, (int)LifeBoard.ActualHeight, rules);
            DrawingHelper.DrawGameBoard(LifeBoard, mainGame);

            playTimer = new System.Timers.Timer(COUNTER_TIME);
			playTimer.Elapsed += TimerEvent;
            playTimer.AutoReset = true;
            playTimer.Enabled = true;
        }

        // When fired, calculate the next turn, redraw the canvas LifeBoard with the updated state
        void AdvanceStep(object sender, RoutedEventArgs e)
        {
            // prevents modification of board while playing
            if (!isPlaying)
			{
                mainGame.GameStep();
                DrawingHelper.RedrawGameBoard(LifeBoard, mainGame);
            }
        }

        // When fired, move mainGame back one step in history, redraw game board
        void ReverseStep(object sender, RoutedEventArgs e)
        {
            // prevents modification of board while playing
            if (!isPlaying)
			{
                mainGame.GoBackStep();
                DrawingHelper.RedrawGameBoard(LifeBoard, mainGame);
            }
        }

        // When fired, finds the position of the cursor, redraws the canvas and then highlights the cell the cursor is over
        // If user was clicking and dragging, then cells will be modified as they move the mouse
        void CanvasMouseMovement(object sender, MouseEventArgs e)
        {
            // prevents needless redrawing while the game is set to play
            if (!isPlaying)
			{
                Point position = Mouse.GetPosition(sender as Canvas);
                // if user clicks and drags, then draw cells as they move the mouse
                if (e.LeftButton == MouseButtonState.Pressed)
				{
                    mainGame.SwitchStatus((int)position.X, (int)position.Y, true);
				}
                DrawingHelper.RedrawGameBoard(LifeBoard, mainGame);
                DrawingHelper.DrawHighlightedCell(LifeBoard, (int)position.X, (int)position.Y, mainGame);
            }
        }

        // When fired, finds the position of the cursor, flips the status of the cell (dead/alive or false/true) and then redraws the canvas
        void CanvasCellClicked(object sender, RoutedEventArgs e)
        {
            // prevents modification of game while game is set to play
            if (!isPlaying)
			{
                Point position = Mouse.GetPosition(LifeBoard);
                mainGame.SwitchStatus((int)position.X, (int)position.Y, false);
                DrawingHelper.RedrawGameBoard(LifeBoard, mainGame);
            }
        }

        // Opens a menu for setting the RuleSet and GameState
        void OpenSetupMenu(object sender, RoutedEventArgs e)
        {
            // stops redrawing while SetupMenu is open
            isPlaying = false; 
            playTimer.Stop();

            SetupMenu setupPage = new SetupMenu(rules, mainGame);
            setupPage.ShowDialog();
            rules = setupPage.NewRuleset();
            int length = setupPage.NewGameStateLength();
            mainGame = new GameState(length, (int)LifeBoard.ActualWidth, (int)LifeBoard.ActualHeight, rules);
            // creates new board, cannot reuse old rectangles
            DrawingHelper.DrawGameBoard(LifeBoard, mainGame);

            // continues redrawing
            playTimer.Start();
        }

        // Make LifeBoard redraw with a new GameStep every COUNTER_TIME time
        void Play(object sender, RoutedEventArgs e)
		{
            isPlaying = true;

        }

        // Stops LifeBoard from automatically updating
        void Stop(object sender, RoutedEventArgs e)
		{
            isPlaying = false;
		}

        // Fires every COUNTER_TIME
        // if enabled by Play, redraws the LifeBoard with a GameStep for mainGame
        void TimerEvent(object source, ElapsedEventArgs e)
		{
            Dispatcher.Invoke(() =>
            {
                if (isPlaying)
                {
                    mainGame.GameStep();
                    DrawingHelper.DrawGameBoard(LifeBoard, mainGame);
                }
            });
        }
    }
}
