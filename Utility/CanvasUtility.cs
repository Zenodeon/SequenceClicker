using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SequenceClicker
{
    public static class CanvasUtility
    {
        public static Point GetCanasPosition(this UIElement element)
        {
            Point point = new Point();
            point.X = Canvas.GetLeft(element);
            point.Y = Canvas.GetTop(element);
            return point;
        }

        public static void SetCanvasPosition(this UIElement element, double x, double y, int z)
        {
            element.SetCanvasPosition(x, y);
            Canvas.SetZIndex(element, z);
        }
        public static void SetCanvasPosition(this UIElement element, double x, double y)
        {
            Canvas.SetLeft(element, x);
            Canvas.SetTop(element, y);
        }
        public static void SetCanvasPosition(this UIElement element, Point position, int z)
            => element.SetCanvasPosition(position.X, position.Y, z);

        public static void SetCanvasPosition(this UIElement element, Point position)
            => element.SetCanvasPosition(position.X, position.Y);
    }
}
