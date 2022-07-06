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

        List<MenuButton> buttons = new List<MenuButton>();

        public MenuPanel()
        {
            InitializeComponent();
            MBControl.ItemsSource = buttons;
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            LocalState.OverlayWindow.CloseMenuPanel();
        }

        public void OpenPanel(IMenuPanelContent panelContent)
        {
            active = true;

            List<MenuButtonBP> buttonBPs = new List<MenuButtonBP>();
            panelContent.AddMenuContent(ref buttonBPs);

            ConstructMenu(buttonBPs);
        }

        public void ClosePanel()
        {
            active = false;
            buttons.Clear();
        }

        private void ConstructMenu(List<MenuButtonBP> buttonBPs)
        {
            int bpCount = buttonBPs.Count;
            for (int i = 0; i < bpCount; i++)
            {
                MenuButton newButton = new MenuButton(buttonBPs[i]);
                buttons.Add(newButton);

                if (i == 0)
                    this.Height = newButton.Height * bpCount;
            }

        }
    }
}
