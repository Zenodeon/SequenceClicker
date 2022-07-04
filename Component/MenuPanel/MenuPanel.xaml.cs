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
    /// Interaction logic for MenuPanel.xaml
    /// </summary>
    public partial class MenuPanel : UserControl
    {
        public bool active { get; set; } = false;

        public MenuPanel()
        {
            InitializeComponent();
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            LocalState.overlayWindow.CloseMenuPanel();
        }
    }
}
