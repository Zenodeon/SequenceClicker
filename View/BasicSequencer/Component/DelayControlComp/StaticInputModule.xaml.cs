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
    /// Interaction logic for StaticInputModule.xaml
    /// </summary>
    public partial class StaticInputModule : UserControl, IInputModule
    {
        public StaticInputModule()
        {
            InitializeComponent();
        }

        public void SetDelayValue(int value)
        {
            SInputCtrl.msDelay = value;
        }

        public int GetDelay()
        {
            return SInputCtrl.msDelay;
        }

        public SaveData GetSaveData()
        {
            return new SaveData(SInputCtrl);
        }

        public void LoadSaveData(SaveData data)
        {
            SInputCtrl.LoadSaveData(data.SInputCtrlSD);
        }

        public struct SaveData
        {
            public DelayInputControl.SaveData SInputCtrlSD;

            public SaveData(DelayInputControl iCtrl)
            {
                SInputCtrlSD = iCtrl.GetSaveData();
            }
        }
    }
}


