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
        List<CursorPoint> csrPoints = new List<CursorPoint>();

        public OverlayWindow()
        {
            InitializeComponent();

            LocalState.MenuPanel = new MenuPanel();

            CursorPoint cursorPoint = new CursorPoint(0, main: true);
            AddCursorPointToCanvas(cursorPoint);
            cursorPoint.SetCanvasPosition(100, 100, 0);
        }

        public void OpenMenuPanel(Point screenPos, CursorPoint cursorPoint)
        {
            MenuPanel panel = LocalState.MenuPanel;

            if (!panel.active)
                CursorSite.Children.Add(panel);

            panel.OpenPanel(cursorPoint);

            panel.SetCanvasPosition(screenPos.X, screenPos.Y, 1);
        }

        public void CloseMenuPanel()
        {
            MenuPanel panel = LocalState.MenuPanel;
            CursorSite.Children.Remove(panel);
            panel.ClosePanel();
        }

        public void AddCursorPoint()
        {
            CursorPoint newCsrPoint = new CursorPoint(csrPoints.Count);
            AddCursorPointToCanvas(newCsrPoint);
            newCsrPoint.SetupMode();
        }

        private void AddCursorPointToCanvas(CursorPoint cursorPoint)
        {
            CursorSite.Children.Add(cursorPoint);
            csrPoints.Add(cursorPoint);
        }

        private void RemoveCursorPointFromCanvas(CursorPoint cursorPoint)
        {
            DLog.Warn("'AddCursorPointToCanvas' Feature Not Done");
            //CursorSite.Children.Remove(cursorPoint);
            //csrPoints.Remove(cursorPoint);
        }
    }
}
