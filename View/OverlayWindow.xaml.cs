﻿using System;
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
        public bool hasInput { get; private set; }

        public bool shown = false;

        private Dictionary<int, ScreenPoint> scrPoints = new Dictionary<int, ScreenPoint>();

        public OverlayWindow()
        {
            InitializeComponent();

            LocalState.MenuPanel = new MenuPanel();

            ScreenPoint screenPoint = new ScreenPoint(0, main: true);
            AddCursorPointToCanvas(screenPoint);

            Point startingPoint = new Point(100, 100);
            screenPoint.SetCanvasPosition(startingPoint - ((Vector)screenPoint.elementSize / 2), 0);
            screenPoint.SetTargetPoint(startingPoint);
        }

        public void OpenMenuPanel(Point screenPos, ScreenPoint screenPoint)
        {
            MenuPanel panel = LocalState.MenuPanel;

            if (!panel.active)
                PointPanel.Children.Add(panel);

            panel.OpenPanel(screenPoint);

            panel.SetCanvasPosition(screenPos.X, screenPos.Y, 1);
        }

        public void CloseMenuPanel()
        {
            MenuPanel panel = LocalState.MenuPanel;
            PointPanel.Children.Remove(panel);
            panel.ClosePanel();
        }

        public void AddCursorPoint()
        {
            ScreenPoint newScreenPoint = new ScreenPoint(scrPoints.Count);
            AddCursorPointToCanvas(newScreenPoint);
            newScreenPoint.SetupMode();
        }

        private void AddCursorPointToCanvas(ScreenPoint cursorPoint)
        {
            PointPanel.Children.Add(cursorPoint);
            scrPoints.Add(cursorPoint.id, cursorPoint);
        }

        private void RemoveCursorPointFromCanvas(ScreenPoint cursorPoint)
        {
            DLog.Warn("RemoveCursorPointToCanvas' Feature Not Done");
            //CursorSite.Children.Remove(cursorPoint);
            //csrPoints.Remove(cursorPoint);
        }

        public ScreenPoint GetPoint(int pointID)
        {
            if (scrPoints.ContainsKey(pointID))
                return scrPoints[pointID];
            else
                return null;
        }

        public void ToggleOverlayVisiblity()
        {
            if (shown)
                HideOverlay();
            else
                ShowOverlay();
        }

        public void ShowOverlay()
        {
            Show();
            shown = true;
        }

        public void HideOverlay()
        {
            Hide();
            shown = false;
        }

        public void IgnoreInput(bool state)
        {
            IntPtr hwnd = new WindowInteropHelper(this).Handle;
            User32API.SetWindowTransparent(hwnd, state);

            hasInput = state;
        }
    }
}
