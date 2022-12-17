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

namespace SequenceClicker.View
{
    /// <summary>
    /// Interaction logic for MenuTab.xaml
    /// </summary>
    public partial class MenuTab : UserControl
    {
        bool active = false;

        public Action<MenuTabAction> OnActionRequest;

        public MenuTab()
        {
            InitializeComponent();
        }

        public void ToggleVisibilty()
        {
            this.StopAnimation(HeightProperty);

            active = !active;
            this.FadeProperty(HeightProperty, active? 20 : 0);
        }

        private void RequestAction(MenuTabAction action)
        {
            OnActionRequest?.Invoke(action);
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            RequestAction(MenuTabAction.OpenFile);
        }

        private void CreateNewFile(object sender, RoutedEventArgs e)
        {
            RequestAction(MenuTabAction.NewFile);
        }

        private void SaveCurrentFile(object sender, RoutedEventArgs e)
        {
            RequestAction(MenuTabAction.SaveFile);
        }

        private void SaveAsCurrentFile(object sender, RoutedEventArgs e)
        {
            RequestAction(MenuTabAction.SaveAsFile);
        }

        public enum MenuTabAction
        {
            OpenFile,
            NewFile,
            SaveFile,
            SaveAsFile
        }
    }
}
