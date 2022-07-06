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

        bool test = false;

        private IntPtr hwnd;
        //private HwndSource? hwndSource;

        public MainWindow()
        {
            DLog.Instantiate();

            InitializeComponent();
            LocalState.MainWindow = this;

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
            test = !test;
            IntPtr hwnd = new WindowInteropHelper(overlayWindow).Handle;
            User32API.SetWindowTransparent(hwnd, test);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                foreach (CursorPoint cursorPoint in LocalState.OverlayWindow.csrPoints)
                {
                    TouchAtPoint(cursorPoint.targetPoint);
                    Thread.Sleep(300);
                }
            });
        }

        private void TouchAtPoint(Point screenPoint)
        {
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
