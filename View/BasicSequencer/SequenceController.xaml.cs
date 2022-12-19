using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SequenceClicker.View
{
    /// <summary>
    /// Interaction logic for SequenceController.xaml
    /// </summary>
    public partial class SequenceController : UserControl
    {
        bool startButtonVisible = true;

        public Action<StateAction> OnActionRequest;

        public SequenceController()
        {
            InitializeComponent();
        }

        private void RequestAction(StateAction action)
        {
            OnActionRequest?.Invoke(action);
        }

        private void MButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (startButtonVisible)
            {
                RequestAction(StateAction.Start);
                ShowStartButton(false);
            }
            else
            {
                RequestAction(StateAction.Pause);
                ShowStartButton(true);
            }
        }

        private void SButton_OnClick(object sender, RoutedEventArgs e)
        {
            RequestAction(StateAction.Stop);

            ShowStartButton(true);
        }

        private void RButton_OnClick(object sender, RoutedEventArgs e)
        {
            RequestAction(StateAction.Restart);

            ShowStartButton(false);
        }

        public void ShowStartButton(bool state)
        {
            if(state)
            {
                startButtonVisible = true;
                mButton.Text = "Start";
            }
            else
            {
                startButtonVisible = false;
                mButton.Text = "Pause";
            }
        }

        public enum StateAction
        {
            Start,
            Pause,
            Stop,
            Restart
        }
    }
}
