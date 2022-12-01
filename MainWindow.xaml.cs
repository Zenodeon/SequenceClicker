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
using SequenceClicker.TouchSim;

namespace SequenceClicker
{
    public partial class MainWindow : Window
    {
        OverlayWindow overlayWindow;

        bool basicSequenceRunning = false;

        private IntPtr hwnd;
        //private HwndSource? hwndSource;

        public MainWindow()
        {
            DLog.Instantiate();

            InitializeComponent();

            LocalState.MainWindow = this;

            TouchInput.Initialize();

            overlayWindow = new OverlayWindow();
            LocalState.OverlayWindow = overlayWindow;
        }

        private async void StartBasicSequence()
        {
            overlayWindow.IgnoreInput(true);

            basicSequenceRunning = true;

            BasicSeq.BeginTask();

            overlayWindow.IgnoreInput(false);
        }

        private void StopBasicSequence()
        {
            basicSequenceRunning = false;

            BasicSeq.StopTask();
        }

        #region UI Interaction

        protected override void OnSourceInitialized(EventArgs e)
        {
            hwnd = new WindowInteropHelper(this).Handle;
            //hwndSource = HwndSource.FromHwnd(hwnd);
            //hwndSource.AddHook(MsgHook);

            //User32API.RegisterHotKey(hwnd, 1, User32DS.HKMod.MOD_CONTROL, Key.P);
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

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            overlayWindow.Close();
            Close();
        }

        private void MinWindow(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void SequenceController_OnClick(object sender, RoutedEventArgs e)
        {
            if (basicSequenceRunning)
                StopBasicSequence();
            else
                StartBasicSequence();
        }

        private void OverlayToggle(object sender, RoutedEventArgs e)
        {
            overlayWindow.ToggleOverlayVisiblity();
        }

        #endregion

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
