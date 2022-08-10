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
    public partial class DelayControl : UserControl, IDelay
    {
        public DelayMode delayMode = DelayMode.Random;

        private int defStaticDelay = 150;
        private int defMinDelay = 150;
        private int defMaxDelay = 300;

        IInputModule activeModule => delayMode == DelayMode.Random ? RInputModl : SInputModl;

        private int finalDelay = -1;

        public DelayControl()
        {
            InitializeComponent();

            SetDefaultValue();
            UpdateModeFormat();
        }

        public void LiveMode(bool state)
        {
            this.IsHitTestVisible = !state;

            if (state)
            {
                LivePanel.Visibility = Visibility.Visible;
                finalDelay = activeModule.GetDelay();
            }

            LivePanel.FadeOpacity(state ? 1 : 0).OnComplete(() =>
            {
                if(!state)
                    LivePanel.Visibility = Visibility.Collapsed;
            });

            LiveDelay.Text = finalDelay + "";
        }

        public void Delay()
        {

        }

        private void SetDefaultValue()
        {
            SInputModl.SetDelayValue(defStaticDelay);
            RInputModl.SetDelayValue(defMinDelay, defMaxDelay);
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
            DelayModeSwitch.Text = text;
        }

        private void SwitchDelayInput(DelayMode delayMode)
        {
            bool isStaticMode = delayMode == DelayMode.Static;
            bool isRandomMode = delayMode == DelayMode.Random;

            SInputModl.Visibility = isStaticMode? Visibility.Visible : Visibility.Collapsed;
            RInputModl.Visibility = isRandomMode ? Visibility.Visible : Visibility.Collapsed;
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
