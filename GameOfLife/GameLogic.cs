using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public static class GameLogic
    {
        public static bool[,] GameStep(bool[,] givenBoard)
        {
            bool[,] newBoard = (bool[,])givenBoard.Clone();

            for (int i = 0; i < givenBoard.GetLength(0); i++) 
            {
                for (int j = 0; j < givenBoard.GetLength(1); j++)
                {
                    NewCellStatus(i, j, givenBoard);
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
            bool status = true;
            int liveNeighbors = 0;

            for (int y = -1; y < 2; y++)
            {
                for (int x = -1; x < 2; x++)
                {
                    if (BoundaryCheck(i, j, y, x, originalArray.GetLength(0), originalArray.GetLength(1)))
                    {
                        if (y != 0 && x != 0 && originalArray[i + y, j + x] == true)
                        {
                            liveNeighbors++;
                        }
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
            bool isValid = true;
            if ((i + plusI) < 0 || (i + plusI) > maxISize - 1)
            {
                isValid = false;
            }

            if ((j + plusJ) < 0 || (j + plusJ) > maxJSize - 1)
            {
                isValid = false;
            }

            return isValid;
        }

    }
}
