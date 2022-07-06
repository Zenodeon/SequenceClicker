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

namespace SequenceClicker.Component
{
    /// <summary>
    /// Interaction logic for MenuButton.xaml
    /// </summary>
    public partial class MenuButton : UserControl
    {
        MenuButtonBP menuButtonBP;

        bool disabled = false;

        public MenuButton(MenuButtonBP menuButtonBP)
        {
            InitializeComponent();
            this.menuButtonBP = menuButtonBP;

            MButton.Content = menuButtonBP.name;
            disabled = menuButtonBP.callback == null;

            if (disabled)
                MButton.Background = Brushes.DimGray;
        }

        private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            OnMouseEvent(ButtonState.Down);
        }

        private void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            OnMouseEvent(ButtonState.Drag);
        }

        private void OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            OnMouseEvent(ButtonState.Up);
        }

        private void OnMouseEvent(ButtonState state)
        {
            if (disabled)
                return;

            if (state != menuButtonBP.callbackOnState)
                return;

            menuButtonBP.callback.Invoke();
            LocalState.overlayWindow.CloseMenuPanel();
        }
    }
}
