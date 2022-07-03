using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using SequenceClicker.API;
using SequenceClicker.Component;

namespace SequenceClicker.View
{
    public partial class OverlayWindow : Window
    {
        public OverlayWindow()
        {
            InitializeComponent();

            CursorPoint cursorPoint = new CursorPoint();

            CursorSite.Children.Add(cursorPoint);
            cursorPoint.Visibility = Visibility.Visible;

            Canvas.SetLeft(cursorPoint, 100);
            Canvas.SetTop(cursorPoint, 100);
            Canvas.SetZIndex(cursorPoint, 0);
        }
    }
}
