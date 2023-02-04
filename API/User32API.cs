using System;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Runtime.InteropServices;

namespace SequenceClicker.API
{
    //Based on https://github.com/oriash93/AutoClicker User32ApiUtils.cs and https://social.msdn.microsoft.com/Forums/en-US/a3cb7db6-5014-430f-a5c2-c9746b077d4f/click-through-windows-and-child-image-issue?forum=wpf

    public static class User32API
    {
        #region Mouse Events
        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        internal static extern bool SetCursorPosition(int x, int y);

        [DllImport("user32.dll", EntryPoint = "mouse_event")]
        internal static extern void ExecuteMouseEvent(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        #endregion

        #region Window Click Through
        public const int WS_EX_TRANSPARENT = 0x00000020;
        public const int GWL_EXSTYLE = (-20);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        public static void SetWindowTransparent(IntPtr hwnd, bool state)
        {
            int extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);

            int newStyleTag = state ? extendedStyle | WS_EX_TRANSPARENT : extendedStyle & ~WS_EX_TRANSPARENT;

            User32API.SetWindowLong(hwnd, GWL_EXSTYLE, newStyleTag);
        }
        #endregion

        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(POINT Point);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder title, int size);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;

            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public POINT(Point point)
            {
                this.x = (int)point.X;
                this.y = (int)point.Y;
            }
        }
    }
}
