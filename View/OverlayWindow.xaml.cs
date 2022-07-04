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

            LocalState.menuPanel = new MenuPanel();

            CursorPoint cursorPoint = new CursorPoint(0);
            CursorPoint cursorPoint1 = new CursorPoint(1);

            CursorSite.Children.Add(cursorPoint);
            CursorSite.Children.Add(cursorPoint1);

            Canvas.SetLeft(cursorPoint, 100);
            Canvas.SetTop(cursorPoint, 100);
            Canvas.SetZIndex(cursorPoint, 0);
            Canvas.SetZIndex(cursorPoint1, -1);
        }

        public void OpenMenuPanel(Point screenPos, CursorPoint cursorPoint)
        {
            MenuPanel panel = LocalState.menuPanel;

            if (!panel.active)
                CursorSite.Children.Add(panel);

            panel.OpenPanel(cursorPoint);

            Canvas.SetLeft(panel, screenPos.X);
            Canvas.SetTop(panel, screenPos.Y);
            Canvas.SetZIndex(panel, 1);
        }

        public void CloseMenuPanel()
        {
            MenuPanel panel = LocalState.menuPanel;
            CursorSite.Children.Remove(panel);
            panel.ClosePanel();
        }

        public void AddCursorPoint()
        {
            DLog.Log("Adding Point : Placeholder");
        }
    }
}
