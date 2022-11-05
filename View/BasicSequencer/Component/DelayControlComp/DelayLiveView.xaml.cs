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
using System.Windows.Media.Animation;

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

        public void DelayLive(Action updateCallback, Action callback)
        {
            LiveDelayVB.FadeProperty(HeightProperty, delayVBHeight);
            LiveDelayProgressVB.FadeProperty(HeightProperty, delayProgressVBHeight);

            LiveBar.FadeProperty(WidthProperty, this.Width, msDuration: targetDelay)
                .OnUpdate(() =>
                {
                    LiveDelayProgress.Text = (int)UUtility.RangedMapClamp((float)LiveBar.Width, 0, (float)this.Width, 0, targetDelay) + "";
                    updateCallback?.Invoke();
                })
                .OnComplete(() =>
                {
                    LiveDelayProgress.Text = (int)targetDelay + "";
                    callback?.Invoke();
                });
        }

        public void DelayLiveOff()
        {
            StopAnimation();

            LiveDelayVB.FadeProperty(HeightProperty, LivePanel.Height);
            LiveDelayProgressVB.FadeProperty(HeightProperty, 0);

            LiveDelayProgress.Text = "";

            LiveBar.Width = 0;
        }

        public void StopAnimation()
        {
            LiveDelayVB.StopAnimation(HeightProperty);
            LiveDelayProgressVB.StopAnimation(HeightProperty);
            LiveBar.StopAnimation(WidthProperty);
        }
    }
}
