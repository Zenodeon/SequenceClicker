﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using SequenceClicker.View;

namespace SequenceClicker.Component
{
    public partial class ScreenPoint : UserControl, IMenuPanelContent
    {
        private OverlayWindow overlayWin => LocalState.OverlayWindow;

        public int id { get; private set; }

        private bool main = false;

        private bool setup = false;
        private bool moving = false;
        //private bool mouseInUse = false;

        public Point targetPoint { get; private set; }
        private Point cursorPosOnElement;

        public Point elementPosition => this.GetCanasPosition() + (Vector)cursorPosOnElement;
        public Point elementSize => new Point(this.Width, this.Height);

        private bool _holding = false;
        private bool holding
        {
            get => _holding;
            set
            {
                _holding = value;
                if (value)
                    this.CaptureMouse();
                else
                    this.ReleaseMouseCapture();
            }
        }

        public ScreenPoint(int id, bool main = false)
        {
            InitializeComponent();

            UpdateID(id);
            this.main = main;
        }

        private void UpdateID(int newID)
        {
            id = newID;
            IDD.Text = id + "";

            if (id == 0)
                main = true;
        }

        private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (LocalState.MainWindow.basicSeqRunning)
                return;

            cursorPosOnElement = e.GetPosition(this);

            switch (e.ChangedButton)
            {
                case MouseButton.Left:
                    holding = true;
                    break;

                case MouseButton.Right:
                    break;
                default:
                    break;
            }
        }

        private void OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (LocalState.MainWindow.basicSeqRunning)
                return;

            switch (e.ChangedButton)
            {
                case MouseButton.Left:
                    holding = false;

                    if (setup)
                        setup = false;

                    moving = false;

                    Vector pointOnElement = (Vector)e.GetPosition(this);
                    targetPoint = e.GetPosition(null) - pointOnElement + ((Vector)elementSize / 2);
                    break;

                case MouseButton.Right:
                    overlayWin.OpenMenuPanel(e.GetPosition(null), this);
                    break;
                default:
                    break;
            }
        }

        private void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (LocalState.MainWindow.basicSeqRunning)
                return;

            if (!holding)
                return;

            if (!setup && !moving && Keyboard.IsKeyDown(Key.LeftAlt))
            {
                holding = false;
                overlayWin.AddCursorPoint();

                return;
            }

            UpdatePosition(e.GetPosition(null));
            moving = true;
        }

        public void SetupMode()
        {
            setup = true;
            holding = true;

            Point centerPoint = (Point)((Vector)elementSize / 2);
            cursorPosOnElement = centerPoint;
            this.SetCanvasPosition((Point)(Mouse.GetPosition(null) - centerPoint));
        }

        private void UpdatePosition(Point mousePos)
        {
            Point newPos = (Point)(mousePos - cursorPosOnElement);
            this.SetCanvasPosition(newPos);
        }

        public void AddMenuContent(ref List<MenuButtonBP> buttons)
        {
            buttons.Add(new MenuButtonBP("Add", overlayWin.AddCursorPoint, ButtonState.Down));
            buttons.Add(new MenuButtonBP("Remove", () => overlayWin.RemoveCursorPoint(this), ButtonState.Up));
        }

        public void SetTargetPoint(Point point)
        {
            targetPoint = point;
        }

        private void SetPointPosition(Point screenPosition)
        {
            Point centerPoint = (Point)((Vector)elementSize / 2);

            screenPosition.X -= centerPoint.X;
            screenPosition.Y -= centerPoint.Y;

            this.SetCanvasPosition(screenPosition);
        }

        public SaveData GetSaveData()
        {
            return new SaveData(id, targetPoint);
        }

        public void LoadSaveData(SaveData saveData)
        {
            UpdateID(saveData.id);
            targetPoint = saveData.point;

            SetPointPosition(targetPoint);
        }

        public struct SaveData
        {
            public int id;
            public Point point;

            public SaveData(int id, Point point)
            {
                this.id = id;
                this.point = point;
            }
        }
    }
}
