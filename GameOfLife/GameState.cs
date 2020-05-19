﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    // A class for holding information about the current run of the game
    // Has methods for retriving a bool[,] that holds the alive/dead status of every cell. 
    // Has methods for retrieving the length of the x/y axis of the bool[,], with each axis of equal length
    public class GameState
    {
        private readonly int cellLengthNum;
        private readonly int cellLengthPixels;
        private bool[,] lifeCells;
        private Stack<bool[,]> lifeCellsHistory;

        // Initializes GameState with a length of the given gameWidth
        // Integer must be greater than 0, an ArgumentOutOfRangeException is thrown if it is less than 0
        public GameState(int gameWidth, int pixelsWidth, int pixelsHeight)
        {
            if (gameWidth <= 0 || pixelsWidth <= 0)
                throw new ArgumentOutOfRangeException("Error creating new GameState: Given Integer less than or equal to 0");
            
            if (pixelsWidth != pixelsHeight)
                throw new ArgumentException("Error creating new GameState: Height and Width are not equal.");

            lifeCellsHistory = new Stack<bool[,]>();
            cellLengthNum = gameWidth;
            cellLengthPixels = pixelsWidth;
            lifeCells = new bool[gameWidth, gameWidth];
        }

        // Returns the length of the both sides of the game board as an int
        public int Length()
        {
            return cellLengthNum;
        }

        // Returns the bool[,] array holding the status of every cell
        public bool[,] GameStatus()
        {
            return lifeCells;
        }

        // Iterates through each cell in a board and returns an array holding the dead/alive status of the new turn
        public void GameStep()
        {
            lifeCellsHistory.Push((bool[,])lifeCells.Clone());
            bool[,] newBoard = new bool[cellLengthNum, cellLengthNum]; // temporary array for holding modifications
            for (int i = 0; i < cellLengthNum; i++)
                for (int j = 0; j < cellLengthNum; j++)
                    newBoard[i, j] = NewCellStatus(i, j);
            lifeCells = newBoard;
        }


        // Moves the lifeCells bool[,] back one step in its history
        public void GoBackStep()
        {
            try
            {
                lifeCells = (bool[,])lifeCellsHistory.Pop().Clone();
            }
            // nothing needs to be done
            catch(InvalidOperationException) 
            { }   
        }

        // If selected cell (int x, int y) is true, set the selected cell to false
        // Otherwise, set the selected cell to true
        public void SwitchStatus(int j, int i)
        {
            int xCell = j / (cellLengthPixels / cellLengthNum);
            int yCell = i / (cellLengthPixels / cellLengthNum);

            if (lifeCells[yCell, xCell] == true)
                lifeCells[yCell, xCell] = false;
            else
                lifeCells[yCell, xCell] = true;
        }

        // Checks the status of nearby cells
        // If currently selected cell (based on 1st and 2nd dimension indexes, i and j respectively) should be alive
        // then return true. return false otherwise
        // Return false otherwise
        private bool NewCellStatus(int i, int j)
        {
            bool status = lifeCells[i, j];
            int liveNeighbors = 0;

            for (int y = -1; y < 2; y++)
                for (int x = -1; x < 2; x++)
                    if (BoundaryCheck(i, j, y, x) && !(y == 0 && x == 0) && lifeCells[i + y, j + x] == true)
                        liveNeighbors++;

            if (liveNeighbors < 2)
                status = false;
            else if (liveNeighbors > 3)
                status = false;
            else if (liveNeighbors == 3)
                status = true;

            return status;
        }

        // Returns true if the examined cell (i + plusI)(j + plusJ) is within the constraints of maxISize and maxJSize
        // Returns false otherwise
        private bool BoundaryCheck(int i, int j, int plusI, int plusJ)
        {
            if ((i + plusI) < 0 || (i + plusI) > cellLengthNum - 1)
                return false;

            if ((j + plusJ) < 0 || (j + plusJ) > cellLengthNum - 1)
                return false;

            return true;
        }
    }
}
