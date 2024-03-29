﻿using System;
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

        private float delayInputValue { get => float.Parse(delayInput.Text); set { delayInput.Text = value.ToString(); } }

        public int msDelay
        {
            get
            {
                if (timeType == TimeType.ms)
                    return (int)delayInputValue;
                else
                    return (int)(delayInputValue * 1000);
            }
            set
            {
                if (timeType == TimeType.ms)
                    delayInputValue = value;
                else
                    delayInputValue = value / 1000;
            }
        }

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

            if (needConverting)
            {
                if (timeType == TimeType.ms)
                    delayInputValue *= 1000;
                else
                    delayInputValue /= 1000;
            }

            TimeModeSwitch.Text = timeType.ToString();
        }

        public enum TimeType
        {
            ms,
            Second
        }

        public SaveData GetSaveData()
        {
            return new SaveData(timeType, delayInput.Text);
        }

        public void LoadSaveData(SaveData data)
        {
            if (IsVaildFloat(data.delayInputValue))
                delayInput.Text = data.delayInputValue;
            else
                delayInput.Text = "0";

            timeType = data.timeType;

            UpdateModeFormat();
        }

        private bool IsVaildFloat(string text)
        {
            try
            {
                float.Parse(text);
                return true;
            }
            catch
            {
                return false;
            }
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

        public struct SaveData
        {
            public TimeType timeType;
            public string delayInputValue;

            public SaveData(TimeType timeType, string delayInputValue)
            {
                this.timeType = timeType;
                this.delayInputValue = delayInputValue;
            }
        }
    }
}
