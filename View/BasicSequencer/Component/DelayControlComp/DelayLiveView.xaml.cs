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
    /// Interaction logic for DelayLiveView.xaml
    /// </summary>
    public partial class DelayLiveView : UserControl
    {
        float delayVBHeight = 21.3f;
        float delayProgressVBHeight = 55;

        int targetDelay = 0;

        public DelayLiveView()
        {
            InitializeComponent();
        }

        public void SetTargetDelay(int delay)
        {
            targetDelay = delay;
            LiveDelay.Text = delay.ToString();
        }

        public void DelayLive(Action action)
        {
            LiveDelayVB.FadeProperty(HeightProperty, delayVBHeight);
            LiveDelayProgressVB.FadeProperty(HeightProperty, delayProgressVBHeight);

            LiveBar.FadeProperty(WidthProperty, this.Width, msDuration: targetDelay)
                .OnUpdate(() =>
                {
                    DLog.Log("Bar Update");
                })
                .OnComplete(() => DLog.Log("Bar Done"));
        }

        public void DelayLiveOff()
        {
            LiveDelayVB.FadeProperty(HeightProperty, LivePanel.Height);
            LiveDelayProgressVB.FadeProperty(HeightProperty, 0);

            LiveBar.Width = 0;
        }
    }
}
