using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    // A class for holding information about the current run of the game, including the state of the board and game rules
    // Has methods for retriving a bool[,] that holds the alive/dead status of every cell. 
    // Has methods for retrieving the length of the x/y axis of the bool[,], with each axis of equal length
    public class GameState
    {
        readonly int CellLengthNum;
        readonly int CellLengthPixels;
        bool[,] LifeCells;
        readonly Stack<bool[,]> LifeCellsHistory;
        readonly LifeRuleset CurrentRules;
        int XIndexOfLastModifiedCell;
        int YIndexOfLastModifiedCell;

        // Initializes GameState with a length of the given gameWidth
        // Integer must be greater than 0, an ArgumentOutOfRangeException is thrown if it is less than 0
        public GameState(int gameWidth, int pixelsWidth, int pixelsHeight, LifeRuleset givenRules)
        {
            if (gameWidth <= 0 || pixelsWidth <= 0)
                throw new ArgumentOutOfRangeException("Error creating new GameState: Given Integer less than or equal to 0");
            
            if (pixelsWidth != pixelsHeight)
                throw new ArgumentException("Error creating new GameState: Height and Width are not equal.");

            LifeCellsHistory = new Stack<bool[,]>();
            CellLengthNum = gameWidth;
            CellLengthPixels = pixelsWidth;
            LifeCells = new bool[gameWidth, gameWidth];
            CurrentRules = givenRules;
            XIndexOfLastModifiedCell = -1;
            YIndexOfLastModifiedCell = -1;
        }

        // Returns the length of the both sides of the game board as an int
        public int Length()
        {
            return CellLengthNum;
        }

        // Returns the bool[,] array holding the status of every cell
        public bool[,] GameStatus()
        {
            return LifeCells;
        }

        // Iterates through each cell in a board and returns an array holding the dead/alive status of the new turn
        public void GameStep()
        {
            LifeCellsHistory.Push((bool[,])LifeCells.Clone());
            bool[,] newBoard = new bool[CellLengthNum, CellLengthNum]; // temporary array for holding modifications
            for (int i = 0; i < CellLengthNum; i++)
                for (int j = 0; j < CellLengthNum; j++)
                    newBoard[i, j] = NewCellStatus(i, j);
            LifeCells = newBoard;
        }


        // Moves the LifeCells bool[,] back one step in its history
        public void GoBackStep()
        {
            try
            {
                LifeCells = (bool[,])LifeCellsHistory.Pop().Clone();
            }
            // nothing needs to be done
            catch(InvalidOperationException) 
            { }   
        }

        // If selected cell (int x, int y) is true, set the selected cell to false
        // Otherwise, set the selected cell to true
        public void SwitchStatus(int j, int i, bool isDragged)
        {
            var xCell = (int)(j / ((double)CellLengthPixels / CellLengthNum));
            var yCell = (int)(i / ((double)CellLengthPixels / CellLengthNum));

            if ((!isDragged || xCell != XIndexOfLastModifiedCell || yCell != YIndexOfLastModifiedCell) &&
                BoundaryCheck(xCell, yCell, 0, 0))
			{
                if (LifeCells[yCell, xCell] == true)
                    LifeCells[yCell, xCell] = false;
                else
                    LifeCells[yCell, xCell] = true;
                XIndexOfLastModifiedCell = xCell;
                YIndexOfLastModifiedCell = yCell;
            }
        }

        // Checks the status of nearby cells
        // If currently selected cell (based on 1st and 2nd dimension indexes, i and j respectively) should be alive
        // then return true. return false otherwise
        // Return false otherwise
        private bool NewCellStatus(int i, int j)
        {
            bool status = LifeCells[i, j];
            int liveNeighbors = 0;

            for (int y = -1; y < 2; y++)
                for (int x = -1; x < 2; x++)
                    if (BoundaryCheck(i, j, y, x) && !(y == 0 && x == 0) && LifeCells[i + y, j + x])
                        liveNeighbors++;

            if (CurrentRules.GetDeathArray().Contains(liveNeighbors))
                status = false;
            else if (CurrentRules.GetGrowthArray().Contains(liveNeighbors))
                status = true;

            return status;
        }

        // Returns true if the examined cell (i + plusI)(j + plusJ) is within the constraints of maxISize and maxJSize
        // Returns false otherwise
        private bool BoundaryCheck(int i, int j, int plusI, int plusJ)
        {
            if ((i + plusI) < 0 || (i + plusI) > CellLengthNum - 1)
                return false;

            if ((j + plusJ) < 0 || (j + plusJ) > CellLengthNum - 1)
                return false;

            return true;
        }
    }
}
