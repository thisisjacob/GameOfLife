using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace GameOfLife
{
    public static class DrawingHelper
    {
        public static void TestDraw(Canvas surface, int width, int height, int fromLeft, int fromTop)
        {
            System.Windows.Shapes.Rectangle test = new System.Windows.Shapes.Rectangle();
            test.Stroke = Brushes.Black;
            test.Fill = Brushes.SkyBlue;
            test.Width = width;
            test.Height = height;
            Canvas.SetTop(surface, fromTop);
            Canvas.SetLeft(surface, fromLeft);
            surface.Children.Add(test);
        }
    }
}
