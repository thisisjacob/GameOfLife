﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace GameOfLife
{
    public static class DrawingHelper
    {
        public static void DrawGameBoard(Canvas surface, bool[,] gameBoard, int cellLengthNum)
        {
            int canvasLength = (int)surface.ActualWidth;
            int cellPixelLength = canvasLength / cellLengthNum;

            surface.Children.Clear(); // resets surface
            for (int i = 0; i < cellLengthNum; i++)
            {
                for (int j = 0; j < cellLengthNum; j++)
                {
                    DrawCell(surface, cellPixelLength, cellPixelLength, j * cellPixelLength, i * cellPixelLength, gameBoard[i, j]);
                }
            }
        }

        public static void DrawCell(Canvas surface, int width, int height, int fromLeft, int fromTop, bool isAlive)
        {
            System.Windows.Shapes.Rectangle test = new System.Windows.Shapes.Rectangle();
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

        public static void DrawHighlightedCell(Canvas surface, int xPos, int yPos, int cellLengthNum)
        {
            int canvasLength = (int)surface.ActualWidth;
            int cellPixelLength = canvasLength / cellLengthNum;
            int xIndex = xPos / cellPixelLength;
            int yIndex = yPos / cellPixelLength;

            System.Windows.Shapes.Rectangle newRect = new System.Windows.Shapes.Rectangle
            {
                Width = cellPixelLength,
                Height = cellPixelLength,
                Fill = Brushes.LightBlue,
                Stroke = Brushes.Black
            };

            surface.Children.Add(newRect);
            Canvas.SetTop(newRect, yIndex * cellPixelLength);
            Canvas.SetLeft(newRect, xIndex * cellPixelLength);
        }
    }
}
