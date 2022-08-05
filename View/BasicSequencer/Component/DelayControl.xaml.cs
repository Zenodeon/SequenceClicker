using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace SequenceClicker.View.BasicSequencer.Component
{
    /// <summary>
    /// Interaction logic for DelayControl.xaml
    /// </summary>
    public partial class DelayControl : UserControl
    {
        public DelayMode delayMode = DelayMode.Random;

        private int staticDelay = 300;
        private int minDelay = 300;
        private int maxDelay = 600;

        public DelayControl()
        {
            InitializeComponent();

            UpdateModeFormat();
        }

        private void OnDelayModeSwitch(object sender, RoutedEventArgs e)
        {
            if (delayMode == DelayMode.Static)
                delayMode = DelayMode.Random;
            else
                delayMode = DelayMode.Static;

            UpdateModeFormat();
        }

        private void UpdateModeFormat()
        {
            SwitchDelayInput(delayMode);

            string text = delayMode == DelayMode.Random ? "Random Range" : "Static";
            DelayModeSwitch.Text = text + " Delay";
        }

        private void SwitchDelayInput(DelayMode delayMode)
        {
            bool isStaticMode = delayMode == DelayMode.Static;
            bool isRandomMode = delayMode == DelayMode.Random;

            SInputCtrl.Visibility = isStaticMode? Visibility.Visible : Visibility.Collapsed;

            RInputCtrl1.Visibility = isRandomMode ? Visibility.Visible : Visibility.Collapsed;
            RInputCtrl2.Visibility = isRandomMode ? Visibility.Visible : Visibility.Collapsed;
        }

        public enum DelayMode
        {
            Static,
            Random
        }

        public string DelayCtrlName
        {
            get { return (string)GetValue(DelayCtrlNameProperty); }
            set { SetValue(DelayCtrlNameProperty, value); }
        }

        public static readonly DependencyProperty DelayCtrlNameProperty =
            DependencyProperty.Register("DelayCtrlName", typeof(string), typeof(DelayControl), new PropertyMetadata("Delay"));
    }
}
