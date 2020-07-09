using System;
using System.IO;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using GameOfLife.FileManagement;
using GameOfLife.Windows.FileManagementWindows;

namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        // Object for holding the current status of the game
        GameState MainGame; 
        // initialized with default Game of Life rules
        LifeRuleset Rules = new LifeRuleset(new int[] { 3 }, new int[] { 2 }, new int[] {0, 1, 4, 5, 6, 7, 8 }); 
        
        bool IsPlaying = false;
        Timer PlayTimer;

        // Constants
        // Default length of LifeBoard
        const int DEFAULT_LENGTH = 32;
        // Default time of counter 
        const int COUNTER_TIME = 1000;

        public MainWindow()
        {
            InitializeComponent();
            LifeBoard.Loaded += InitializeProgram;
			Closed += MainWindow_Closed;
        }

        // Misc. actions that must be done when user closes the program
        private void MainWindow_Closed(object sender, EventArgs e)
		{
			if (PlayTimer != null)
			{
                PlayTimer.Dispose();
			}
		}

		// When fired, erases all the current graphics on the LifeBoard canvas and creates the graphics for the game
		void InitializeProgram(object sender, EventArgs e)
        {
            MainGame = new GameState(DEFAULT_LENGTH, (int)LifeBoard.ActualWidth, (int)LifeBoard.ActualHeight, Rules);
            DrawingHelper.DrawGameBoard(LifeBoard, MainGame);

            PlayTimer = new Timer(COUNTER_TIME);
			PlayTimer.Elapsed += TimerEvent;
            PlayTimer.AutoReset = true;
            PlayTimer.Enabled = true;
        }

        // When fired, calculate the next turn, redraw the canvas LifeBoard with the updated state
        void AdvanceStep(object sender, RoutedEventArgs e)
        {
            // prevents modification of board while playing
            if (!IsPlaying)
			{
                MainGame.GameStep();
                DrawingHelper.RedrawGameBoard(LifeBoard, MainGame);
            }
        }

        // When fired, move mainGame back one step in history, redraw game board
        void ReverseStep(object sender, RoutedEventArgs e)
        {
            // prevents modification of board while playing
            if (!IsPlaying)
			{
                MainGame.GoBackStep();
                DrawingHelper.RedrawGameBoard(LifeBoard, MainGame);
            }
        }

        // Captures and releases capture of mouse for LifeBoard
        void ButtonLeave(object sender, RoutedEventArgs e)
		{
            if (LifeBoard.IsEnabled)
			{
                LifeBoard.CaptureMouse();
                LifeBoard.ReleaseMouseCapture();
            }
		}

        // When fired, finds the position of the cursor, redraws the canvas and then highlights the cell the cursor is over
        // If user was clicking and dragging, then cells will be modified as they move the mouse
        void CanvasMouseMovement(object sender, MouseEventArgs e)
        {
            // prevents needless redrawing while the game is set to play
            if (!IsPlaying)
			{
                Point position = Mouse.GetPosition(sender as Canvas);
                // if user clicks and drags, then draw cells as they move the mouse
                if (e.LeftButton == MouseButtonState.Pressed)
				{
                    MainGame.SwitchStatus((int)position.X, (int)position.Y, true);
				}
                DrawingHelper.RedrawGameBoard(LifeBoard, MainGame);
                DrawingHelper.DrawHighlightedCell(LifeBoard, (int)position.X, (int)position.Y, MainGame);
            }
        }

        // Redraws LifeBoard when mouse exits LifeBoard.
        // Has the effect of removing cell selector
        void CanvasMouseLeftItem(object sender, MouseEventArgs e)
		{
            DrawingHelper.RedrawGameBoard(LifeBoard, MainGame);
		}

        // When fired, finds the position of the cursor, flips the status of the cell (dead/alive or false/true) and then redraws the canvas
        void CanvasCellClicked(object sender, MouseEventArgs e)
        {
            // prevents modification of game while game is set to play
            if (!IsPlaying)
			{
                Point position = Mouse.GetPosition(LifeBoard);
                MainGame.SwitchStatus((int)position.X, (int)position.Y, false);
                DrawingHelper.RedrawGameBoard(LifeBoard, MainGame);
            }
        }

        // Opens a menu for setting the RuleSet and GameState
        void OpenSetupMenu(object sender, RoutedEventArgs e)
        {
            // stops redrawing while SetupMenu is open
            IsPlaying = false; 
            PlayTimer.Stop();

            SetupMenu setupPage = new SetupMenu(Rules, MainGame);
            setupPage.ShowDialog();
            Rules = setupPage.NewRuleset();
            int length = setupPage.NewGameStateLength();
            MainGame = new GameState(length, (int)LifeBoard.ActualWidth, (int)LifeBoard.ActualHeight, Rules);
            // creates new board, cannot reuse old rectangles
            DrawingHelper.DrawGameBoard(LifeBoard, MainGame);

            // continues redrawing
            PlayTimer.Start();
        }

        // Make LifeBoard redraw with a new GameStep every COUNTER_TIME time
        void Play(object sender, RoutedEventArgs e)
		{
            IsPlaying = true;
        }

        // Stops LifeBoard from automatically updating
        void Stop(object sender, RoutedEventArgs e)
		{
            IsPlaying = false;
		}

        // Opens a LoadFile window, sets Rules to its results
        void LoadClick(object sender, RoutedEventArgs e)
		{
            LoadFile window = new LoadFile(Rules);
            window.ShowDialog();
            Rules = window.ReturnResult();
		}

        // Opens a SaveFile window
        void SaveClick(object sender, RoutedEventArgs e)
		{
            SaveFile window = new SaveFile();
            window.ShowDialog();
		}

        // Fires every COUNTER_TIME
        // if enabled by Play, redraws the LifeBoard with a GameStep for mainGame
        void TimerEvent(object source, ElapsedEventArgs e)
		{
            Dispatcher.Invoke(() =>
            {
                if (IsPlaying)
                {
                    MainGame.GameStep();
                    DrawingHelper.DrawGameBoard(LifeBoard, MainGame);
                }
            });
        }

        // stops playing, creates a new timer, sets it to the value of sender (assumed to be a slider)
        void ChangeTimerSpeed(object sender, RoutedEventArgs e)
		{
            if (PlayTimer != null)
			{
                var givenObject = (Slider)sender;
                var isPlayingBeforeCall = IsPlaying;
                IsPlaying = false;
                PlayTimer.Close();
                PlayTimer = new Timer(givenObject.Value);
                PlayTimer.Elapsed += TimerEvent;
                PlayTimer.AutoReset = true;
                PlayTimer.Enabled = true;
                IsPlaying = isPlayingBeforeCall;
            }
        }
    }
}
