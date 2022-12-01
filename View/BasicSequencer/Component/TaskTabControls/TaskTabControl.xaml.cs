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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SequenceClicker.View.BasicSequencer.Component
{
    /// <summary>
    /// Interaction logic for TaskTabControl.xaml
    /// </summary>
    public partial class TaskTabControl : UserControl
    {
        public Action<TTAction> OnActionRequest;

        public TaskTabControl()
        {
            InitializeComponent();
        }

        private void RequestAction(TTAction action)
        {
            OnActionRequest?.Invoke(action);
        }

        private void MoveUp(object sender, RoutedEventArgs e)
        {
            RequestAction(TTAction.MoveUp);
        }

        private void RemoveSelf(object sender, RoutedEventArgs e)
        {
            RequestAction(TTAction.RemoveSelf);
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            RequestAction(TTAction.Add);
        }

        private void DuplicateSelf(object sender, RoutedEventArgs e)
        {
            RequestAction(TTAction.DuplicateSelf);
        }

        private void MoveDown(object sender, RoutedEventArgs e)
        {
            RequestAction(TTAction.MoveDown);
        }

        public enum TTAction
        {
            MoveUp,
            RemoveSelf,
            Add,
            DuplicateSelf,
            MoveDown
        }
    }
}
