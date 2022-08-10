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
    /// Interaction logic for TaskTab.xaml
    /// </summary>
    public partial class TaskTab : UserControl
    {
        public ContentPresenter frame { get; set; }
        public int taskIndex { get; set; }

        public TaskTab()
        {
            InitializeComponent();
        }
    }
}
