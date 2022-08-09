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
    /// Interaction logic for RandomInputModule.xaml
    /// </summary>
    public partial class RandomInputModule : UserControl, IInputModule
    {
        public RandomInputModule()
        {
            InitializeComponent();
        }

        public void SetDelayValue(float minValue, float maxValue)
        {
            RInputCtrl1.inputDelay = minValue;
            RInputCtrl2.inputDelay = maxValue;
        }

        public int GetDelay()
        {
            return 0;
        }
    }
}
