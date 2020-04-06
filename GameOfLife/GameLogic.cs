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

                }
            }

            return newBoard;
        }

        // Checks the status of nearby cells
        // If currently selected cell (based on x and y int) should be alive or born, return true
        // Return false otherwise
        public static bool NewCellStatus(int x, int y, bool[,] originalArray)
        {
            bool status;

            return true;
        }

    }
}
