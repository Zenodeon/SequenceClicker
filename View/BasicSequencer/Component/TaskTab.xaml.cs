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

        private List<IDelay> delayModules = new List<IDelay>();

        public TaskTab()
        {
            InitializeComponent();
            AddDelayModules();
        }

        private void AddDelayModules()
        {
            delayModules.Add(SDelay);
            delayModules.Add(HDelay);
        }

        public void LiveMode(bool state)
        {
            TTIndicator.FadeProperty(OpacityProperty, state ? 1 : 0.4f, 150);

            foreach (IDelay module in delayModules)
                module.LiveMode(state);
        }

        public void RunTask()
        {
            SDelay.Delay(() => HDelay.Delay(() => OnTaskExecuted()));
        }

        public void OnTaskExecuted()
        {

        }
    }
}
