using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GameOfLife
{
    public static class DrawingHelper
    {
        // Fills the canvas with new and equally sized rectangles representing the cells of the Game of Life
        public static void DrawGameBoard(Canvas surface, GameState givenGame)
        {
            int canvasLength = (int)surface.ActualWidth;
            int cellPixelLength = canvasLength / givenGame.Length();

            surface.Children.Clear(); // resets surface
            for (int i = 0; i < givenGame.Length(); i++)
            {
                for (int j = 0; j < givenGame.Length(); j++)
                {
                    DrawCell(surface, cellPixelLength, cellPixelLength, j * cellPixelLength, i * cellPixelLength, givenGame.GameStatus()[i, j]);
                }
            }
        }

        // Redraws all the items without creating new rectangle objects
        // Essentially just sets their fill and border colors to match their current state (alive/dead)
        public static void RedrawGameBoard(Canvas surface, bool[,] gameBoard, int cellLengthNum) 
        {

            for (int i = 0; i < cellLengthNum; i++)
            {
                for (int j = 0; j < cellLengthNum; j++)
                {
                    int surfaceChildIndex = (i * cellLengthNum) + j;
                    if (surface.Children[surfaceChildIndex] is Rectangle)
                    {
                        RedrawCell((surface.Children[surfaceChildIndex] as Rectangle), gameBoard[i, j]);
                    }
                    else
                    {
                        throw new NotSupportedException("Children in " + surface.Name + " is not a Rectangle item"); // item in children is not a rectangle, throw exception
                    }
                }
            }

        }

        // Creates a new rectangle, sets its appearance based on its isAlive status, adds to to surface
        public static void DrawCell(Canvas surface, int width, int height, int fromLeft, int fromTop, bool isAlive)
        {
            Rectangle test = new Rectangle();
            if (isAlive == true)
            {
                test.Stroke = Brushes.DarkGray;
                test.Fill = Brushes.Black;
            }
            else
            {
                test.Stroke = Brushes.DarkGray;
                test.Fill = Brushes.White;
            }
            test.Width = width;
            test.Height = height;
            surface.Children.Add(test);
            Canvas.SetTop(test, fromTop);
            Canvas.SetLeft(test, fromLeft);
        }

        // Sets givenCell to match the theme color with its isAlive status
        public static void RedrawCell(Rectangle givenCell, bool isAlive)
        {
            if (isAlive)
            {
                givenCell.Stroke = Brushes.DarkGray;
                givenCell.Fill = Brushes.Black;
            }
            else
            {
                givenCell.Stroke = Brushes.DarkGray;
                givenCell.Fill = Brushes.White;
            }
        }

        // Finds the rectangle matching the xPos and yPos given, sets its color to differentiate it from other cells 
        public static void DrawHighlightedCell(Canvas surface, int xPos, int yPos, int cellLengthNum)
        {
            int canvasLength = (int)surface.ActualWidth;
            int cellPixelLength = canvasLength / cellLengthNum;
            int xIndex = xPos / cellPixelLength;
            int yIndex = yPos / cellPixelLength;
            int surfaceChildIndex = (yIndex * cellLengthNum) + xIndex;

            if (surface.Children[surfaceChildIndex] is Rectangle)
            {
                (surface.Children[surfaceChildIndex] as Rectangle).Fill = Brushes.LightBlue;
            }
            else
            {
                throw new NotSupportedException("Children in " + surface.Name + " is not a Rectangle item"); // item in children is not a rectangle, throw exception
            }

        }
    }
}
