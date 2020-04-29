using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace GameOfLife
{
    public static class GameLogic
    {
        public static bool[,] GameStep(bool[,] givenBoard)
        {
            bool[,] newBoard = new bool[givenBoard.GetLength(0), givenBoard.GetLength(1)];


            for (int i = 0; i < givenBoard.GetLength(0); i++) 
            {
                for (int j = 0; j < givenBoard.GetLength(1); j++)
                {
                    newBoard[i, j] = NewCellStatus(i, j, givenBoard);
                }
            }

            return newBoard;
        }

        // Checks the status of nearby cells
        // If currently selected cell (based on 1st and 2nd dimension indexes, i and j respectively) should be alive
        // then return true. return false otherwise
        // Return false otherwise
        public static bool NewCellStatus(int i, int j, bool[,] originalArray)
        {
            bool status = originalArray[i, j];
            int liveNeighbors = 0;

            for (int y = -1; y < 2; y++)
            {
                for (int x = -1; x < 2; x++)
                {
                    if (BoundaryCheck(i, j, y, x, originalArray.GetLength(0), originalArray.GetLength(1)) && !(y == 0 && x == 0) && originalArray[i + y, j + x] == true)
                    {
                        liveNeighbors++;
                    }
                }
            }

            if (liveNeighbors < 2)
            {
                status = false;
            }
            else if (liveNeighbors > 3)
            {
                status = false;
            }
            else if (liveNeighbors == 3)
            {
                status = true;
            }


            return status;
        }


        public static bool BoundaryCheck(int i, int j, int plusI, int plusJ, int maxISize, int maxJSize)
        {

            if ((i + plusI) < 0 || (i + plusI) > maxISize - 1)
            {
                return false;
            }

            if ((j + plusJ) < 0 || (j + plusJ) > maxJSize - 1)
            {
                return false;
            }

            return true;
        }

        // 
        public static bool[,] SwitchStatus(Canvas surface, bool[,] gameBoard, int x, int y, int cellLengthPixels)
        {
            bool[,] newGameBoard = (bool[,])gameBoard.Clone();
            int xCell = x / cellLengthPixels;
            int yCell = y / cellLengthPixels;


            if (gameBoard[yCell, xCell] == true)
            {
                newGameBoard[yCell, xCell] = false;
            }
            else
            {
                newGameBoard[yCell, xCell] = true;
            }

            return newGameBoard;
        }
    }
}
