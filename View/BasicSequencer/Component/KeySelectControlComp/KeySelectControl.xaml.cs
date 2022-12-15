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
    /// Interaction logic for KeySelectControl.xaml
    /// </summary>
    public partial class KeySelectControl : UserControl
    {
        public KeySelectControl()
        {
            InitializeComponent();
        }

        public int GetPointID()
        {
            return int.Parse(pointerID.Text);
        }

        #region TextInput Filtering
        private readonly Regex allowedNumericRegex = new Regex("[^0-9]+");

        private int maxTextLength = 2;

        private string lastString;

        private bool IsNumericString(string text)
        {
            return allowedNumericRegex.IsMatch(text);
        }

        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            bool handle = IsNumericString(e.Text);

            if (!handle)
            {
                if (textBox.Text.Length >= maxTextLength)
                    handle = true;
            }

            e.Handled = handle;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (e.Key == Key.Return)
            {
                this.LoseFocus();

                FormatTextBox(textBox);
            }

            if (e.Key == Key.Escape)
            {
                textBox.Text = lastString;

                this.LoseFocus();
            }
        }

        private void OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            lastString = textBox.Text;
        }

        private void OnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            FormatTextBox(textBox);
        }

        private void FormatTextBox(TextBox textBox)
        {
            if (textBox.Text == "")
                textBox.Text = 0 + "";

            textBox.Text = int.Parse(textBox.Text) + "";
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

        public SaveData GetSaveData()
        {
            return new SaveData(pointerID.Text, InputTypeSwitch);
        }

        public void LoadSaveData(SaveData data)
        {
            if (IsVaildInt(data.pointerID))
                pointerID.Text = data.pointerID;
            else
                pointerID.Text = "0";

            SInputModl.LoadSaveData(data.sInputModlSD);
            RInputModl.LoadSaveData(data.rInputModlSD);

            UpdateModeFormat();
        }

        private bool IsVaildInt(string text)
        {
            try
            {
                int.Parse(text);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public struct SaveData
        {
            public string pointerID;

            public DelayInputControl.SaveData inputTypeSwitchSD;

            public SaveData(string pointerID, DelayInputControl inputTypeSwitch)
            {
                this.pointerID = pointerID;

                inputTypeSwitchSD = inputTypeSwitch.GetSaveData();
            }
        }
    }
}
