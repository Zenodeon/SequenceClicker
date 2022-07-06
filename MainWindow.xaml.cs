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

            TouchInjector.InitializeTouchInjection();

            overlayWindow = new OverlayWindow();
            overlayWindow.Show();
            LocalState.OverlayWindow = overlayWindow;
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
            // Touch Down Simulate
            PointerTouchInfo contact = MakePointerTouchInfo((int)screenPoint.X, (int)screenPoint.Y, 1);
            PointerFlags oFlags = PointerFlags.DOWN | PointerFlags.INRANGE | PointerFlags.INCONTACT;
            contact.PointerInfo.PointerFlags = oFlags;
            bool bIsSuccess = TouchInjector.InjectTouchInput(1, new[] { contact });

            Thread.Sleep(100);

            // Touch Up Simulate
            contact.PointerInfo.PointerFlags = PointerFlags.UP;
            TouchInjector.InjectTouchInput(1, new[] { contact });
        }

        private PointerTouchInfo MakePointerTouchInfo(int x, int y, int radius, uint orientation = 0, uint pressure = 256)
        {
            PointerTouchInfo contact = new PointerTouchInfo();

            uint unPointerId = IdGenerator.GetUinqueUInt();
            contact.PointerInfo.PointerId = unPointerId;

            contact.PointerInfo.pointerType = PointerInputType.TOUCH;

            contact.TouchFlags = TouchFlags.NONE;
            contact.Orientation = orientation;
            contact.Pressure = pressure;
            contact.TouchMasks = TouchMask.CONTACTAREA | TouchMask.ORIENTATION | TouchMask.PRESSURE;

            contact.PointerInfo.PtPixelLocation.X = x - 8;
            contact.PointerInfo.PtPixelLocation.Y = y - 8;

            contact.ContactArea.left = x - radius;
            contact.ContactArea.right = x + radius;
            contact.ContactArea.top = y - radius;
            contact.ContactArea.bottom = y + radius;

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
