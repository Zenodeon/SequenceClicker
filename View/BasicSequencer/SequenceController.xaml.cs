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
                mButton.Text = "Pause";
            }
            else
            {
                RequestAction(StateAction.Pause);
                mButton.Text = "Start";
            }

            startButtonVisible = !startButtonVisible;
        }

        private void SButton_OnClick(object sender, RoutedEventArgs e)
        {
            RequestAction(StateAction.Stop);
        }

        private void RButton_OnClick(object sender, RoutedEventArgs e)
        {
            RequestAction(StateAction.Restart);
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
