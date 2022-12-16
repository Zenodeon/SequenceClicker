using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace SequenceClicker
{
    public static class ContentElementUtility
    {
        public static DoubleAnimation FadeProperty(this ContentElement element, DependencyProperty dp, double toValue, int msDuration = 150)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = (double)element.GetValue(dp),
                To = toValue,
                Duration = TimeSpan.FromMilliseconds(msDuration),
                FillBehavior = FillBehavior.Stop
            };

            animation.OnComplete(() => element.SetValue(dp, toValue));
           
            element.BeginAnimation(dp, animation);

            return animation;
        }

        public static void StopAnimation(this ContentElement element, DependencyProperty dp)
        {
            element.BeginAnimation(dp, null);
        }
    }
}
