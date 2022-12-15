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

        public void SetDelayValue(int minValue, int maxValue)
        {
            RInputCtrl1.msDelay = minValue;
            RInputCtrl2.msDelay = maxValue;
        }

        public int GetDelay()
        {
            return new Random().Next(RInputCtrl1.msDelay, RInputCtrl2.msDelay); ;
        }

        public SaveData GetSaveData()
        {
            return new SaveData(RInputCtrl1, RInputCtrl2);
        }

        public void LoadSaveData(SaveData data)
        {
            RInputCtrl1.LoadSaveData(data.rInputCtrl1SD);
            RInputCtrl2.LoadSaveData(data.rInputCtrl2SD);
        }

        public struct SaveData
        {
            public DelayInputControl.SaveData rInputCtrl1SD;
            public DelayInputControl.SaveData rInputCtrl2SD;

            public SaveData(DelayInputControl iCtrl1, DelayInputControl iCtrl2)
            {
                rInputCtrl1SD = iCtrl1.GetSaveData();
                rInputCtrl2SD = iCtrl2.GetSaveData();
            }
        }
    }
}
