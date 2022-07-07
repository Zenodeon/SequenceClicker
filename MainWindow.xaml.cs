global using DebugLogger.Wpf;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using SequenceClicker.API;
using SequenceClicker.View;
using SequenceClicker.Component;

namespace SequenceClicker
{
    public partial class MainWindow : Window
    {
        OverlayWindow overlayWindow;

        bool overlayWinHasInput = false;

        private IntPtr hwnd;
        //private HwndSource? hwndSource;

        public MainWindow()
        {
            DLog.Instantiate();

            InitializeComponent();
            LocalState.MainWindow = this;

            TouchInjector.InitializeTouchInjection(feedbackMode: TouchFeedback.INDIRECT);

            overlayWindow = new OverlayWindow();
            overlayWindow.Show();
            LocalState.OverlayWindow = overlayWindow;

            Touch.FrameReported += touchTest;
        }

        private void touchTest(object sender, TouchFrameEventArgs e)
        {
            foreach (System.Windows.Input.TouchPoint tp in e.GetTouchPoints(null))
            {
                DLog.Log($"{tp.Position} : {tp.Action} : {tp.Size}");
            }
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            hwnd = new WindowInteropHelper(this).Handle;
            //hwndSource = HwndSource.FromHwnd(hwnd);
            //hwndSource.AddHook(MsgHook);

            //User32API.RegisterHotKey(hwnd, 1, User32DS.HKMod.MOD_CONTROL, Key.P);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DisableOverlayWindowInput(!overlayWinHasInput);
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DisableOverlayWindowInput(true);

            await Task.Run(() =>
            {
                foreach (CursorPoint cursorPoint in LocalState.OverlayWindow.csrPoints)
                {
                    TouchAtPoint(cursorPoint.id, cursorPoint.targetPoint);
                    Thread.Sleep(300);
                }
            });

            DisableOverlayWindowInput(false);
        }

        private void TouchAtPoint(int id, Point screenPoint)
        {
            IntPtr targetWindow = User32API.WindowFromPoint(new User32API.POINT(screenPoint));
            User32API.SetForegroundWindow(targetWindow);

            return;
            // Touch Down Simulate
            PointerTouchInfo contact = MakePointerTouchInfo(id, (int)screenPoint.X - 7, (int)screenPoint.Y - 7);
            PointerFlags oFlags = PointerFlags.INRANGE | PointerFlags.INCONTACT | PointerFlags.DOWN;
            contact.PointerInfo.PointerFlags = oFlags;
            bool bIsSuccess = TouchInjector.InjectTouchInput(1, new[] { contact });

            int timer = 0;

            while(timer != 100)
            {
                contact.Move(1, 1);
                contact.PointerInfo.PointerFlags = PointerFlags.INRANGE | PointerFlags.INCONTACT | PointerFlags.UPDATE;
                TouchInjector.InjectTouchInput(1, new[] { contact });

                timer++;
                Thread.Sleep(1);
            }

            //Touch Up Simulate
            contact.PointerInfo.PointerFlags = PointerFlags.UP;
            TouchInjector.InjectTouchInput(1, new[] { contact });
        }

        private PointerTouchInfo MakePointerTouchInfo(int id, int x, int y, uint pressure = 32000)
        {
            PointerTouchInfo contact = new PointerTouchInfo();

            contact.PointerInfo.PointerId = (uint)id;

            contact.PointerInfo.pointerType = PointerInputType.TOUCH;

            contact.TouchFlags = TouchFlags.NONE;

            contact.TouchMasks = TouchMask.PRESSURE;
            contact.Pressure = pressure;

            contact.PointerInfo.PtPixelLocation.X = x;
            contact.PointerInfo.PtPixelLocation.Y = y;

            return contact;
        }

        private void DisableOverlayWindowInput(bool state)
        {
            IntPtr hwnd = new WindowInteropHelper(overlayWindow).Handle;
            User32API.SetWindowTransparent(hwnd, state);

            overlayWinHasInput = state;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            overlayWindow.Close();
        }

        private void Window_GotTouchCapture(object sender, TouchEventArgs e)
        {
            
            //foreach(System.Windows.Input.TouchPoint tp in e.GetIntermediateTouchPoints(null))
            //{
            //    DLog.Log($"{tp.Position} : {tp.Action} : {tp.Size}");
            //}
        }

        //private IntPtr MsgHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        //{
        //    string hotkeyId = wParam.ToString();

        //    if (msg == User32DS.WM_HOTKEY)
        //    {
        //        DLog.Log(msg + " : " + hotkeyId);
        //        handled = true;
        //    }
        //    //if (msg == Constants.WM_HOTKEY && hotkeyId == Constants.START_HOTKEY_ID || hotkeyId == Constants.STOP_HOTKEY_ID || hotkeyId == Constants.TOGGLE_HOTKEY_ID)
        //    //{
        //    //    int virtualKey = ((int)lParam >> 16) & 0xFFFF;
        //    //    if (virtualKey == SettingsUtils.CurrentSettings.HotkeySettings.StartHotkey.VirtualKeyCode && CanStartOperation())
        //    //    {
        //    //        StartCommand_Execute(null, null);
        //    //    }
        //    //    if (virtualKey == SettingsUtils.CurrentSettings.HotkeySettings.StopHotkey.VirtualKeyCode && clickTimer.Enabled)
        //    //    {
        //    //        StopCommand_Execute(null, null);
        //    //    }
        //    //    if (virtualKey == SettingsUtils.CurrentSettings.HotkeySettings.ToggleHotkey.VirtualKeyCode && CanStartOperation() | clickTimer.Enabled)
        //    //    {
        //    //        ToggleCommand_Execute(null, null);
        //    //    }
        //    //    handled = true;
        //    //}
        //    return IntPtr.Zero;
        //}
    }
}
