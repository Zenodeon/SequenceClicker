global using DebugLogger.Wpf;

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
using System.Windows.Navigation;
using System.Windows.Shapes;

using SequenceClicker.View;
using SequenceClicker.API;

namespace SequenceClicker
{
    public partial class MainWindow : Window
    {
        OverlayWindow overlayWindow;

        bool test = false;

        public MainWindow()
        {
            DLog.Instantiate();

            InitializeComponent();

            overlayWindow = new OverlayWindow();
            overlayWindow.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            test = !test;
            IntPtr hwnd = new WindowInteropHelper(overlayWindow).Handle;
            User32API.SetWindowTransparent(hwnd, test);
        }
    }
}
