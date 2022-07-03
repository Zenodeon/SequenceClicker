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

namespace SequenceClicker.Component
{
    /// <summary>
    /// Interaction logic for CursorPoint.xaml
    /// </summary>
    public partial class CursorPoint : UserControl
    {
        bool holding = false;

        public CursorPoint()
        {
            InitializeComponent();
        }

        private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            switch(e.ChangedButton)
            {
                case MouseButton.Left:
                    holding = true;
                    CaptureMouse();
                    break;
                default:
                    break;
            }      
        }

        private void OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            switch (e.ChangedButton)
            {
                case MouseButton.Left:
                    holding = false;
                    ReleaseMouseCapture();
                    break;
                default:
                    break;
            }
        }

        private void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (holding)
                UpdatePosition(e.GetPosition(null));
        }

        private void UpdatePosition(Point mousePos)
        {
            Canvas.SetLeft(this, mousePos.X - (RenderSize.Width / 2));
            Canvas.SetTop(this, mousePos.Y - (RenderSize.Height / 2));
        }
    }
}
