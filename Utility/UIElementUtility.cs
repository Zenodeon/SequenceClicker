using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace SequenceClicker
{
    public static class UIElementUtility
    {
        public static DoubleAnimation FadeProperty(this UIElement element, DependencyProperty dp, double value, int msDuration = 150)
        {
            var animation = new DoubleAnimation
            {
                From = (double)element.GetValue(dp),
                To = value,
                Duration = TimeSpan.FromMilliseconds(msDuration),
                FillBehavior = FillBehavior.Stop
            };

            animation.OnComplete(() => element.SetValue(dp, value));
           
            element.BeginAnimation(dp, animation);

            return animation;
        }

        public static void OnComplete(this DoubleAnimation animation, Action action)
        {
            animation.Completed += (o, e) => action();
        }

        public static void OnUpdate(this DoubleAnimation animation, Action action)
        {
            animation.Changed += (o, e) => action();
        }
    }
}
