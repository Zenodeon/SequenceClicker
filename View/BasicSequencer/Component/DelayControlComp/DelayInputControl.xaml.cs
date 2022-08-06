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
    /// Interaction logic for DelayInputControl.xaml
    /// </summary>
    public partial class DelayInputControl : UserControl
    {
        public TimeType timeType = TimeType.ms;

        public float inputDelay { get => float.Parse(delayInput.Text); set { delayInput.Text = value + ""; } }

        public DelayInputControl()
        {
            InitializeComponent();
        }

        private void OnTimeModeSwitch(object sender, RoutedEventArgs e)
        {
            if (timeType == TimeType.ms)
                timeType = TimeType.Second;
            else
                timeType = TimeType.ms;

            UpdateModeFormat();
        }

        private void UpdateModeFormat()
        {
            bool needConverting = delayInput.Text.Contains('.') || Keyboard.IsKeyDown(Key.LeftShift);

            if(needConverting)
            {
                if (timeType == TimeType.ms)
                    inputDelay *= 1000;
                else
                    inputDelay /= 1000;
            }

            TimeModeSwitch.Text = timeType.ToString();
        }

        public enum TimeType
        {
            ms,
            Second
        }

        #region TextInput Filtering
        private readonly Regex allowedMSNumericRegex = new Regex("[^0-9]+");
        private readonly Regex allowedSecNumericRegex = new Regex("[^0-9.]+");
        private bool IsNumericString(string text)
        {
            if (timeType == TimeType.ms)
                return allowedMSNumericRegex.IsMatch(text);
            else
                return allowedSecNumericRegex.IsMatch(text);
        }

        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsNumericString(e.Text);
        }

        private void OnTextBoxPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsNumericString(text))
                    e.CancelCommand();
            }
            else
                e.CancelCommand();
        }
        #endregion
    }
}
