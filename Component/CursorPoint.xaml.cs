using System;
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
    public partial class CursorPoint : UserControl, IMenuPanelContent
    {
        int id;
        private bool main = false;

        private bool setup = false;
        private bool moving = false;
        //private bool mouseInUse = false;

        private Point cursorPosOnElement;

        private Point elementSize => new Point(this.Width, this.Height);

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

        public CursorPoint(int id, bool main = false)
        {
            InitializeComponent();
            this.id = id;
            IDD.Text = id + "";
            this.main = main;
        }

        private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
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
            switch (e.ChangedButton)
            {
                case MouseButton.Left:
                    holding = false;

                    if (setup)
                        setup = false;

                    moving = false;
                    break;

                case MouseButton.Right:
                    LocalState.overlayWindow.OpenMenuPanel(e.GetPosition(null), this);
                    break;
                default:
                    break;
            }
        }

        private void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (!holding)
                return;

            if (!setup && !moving && Keyboard.IsKeyDown(Key.LeftAlt))
            {
                holding = false;
                LocalState.overlayWindow.AddCursorPoint();

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
            buttons.Add(new MenuButtonBP("Add", LocalState.overlayWindow.AddCursorPoint, ButtonState.Down));
            buttons.Add(new MenuButtonBP("Remove", null, ButtonState.Up));
        }
    }
}
