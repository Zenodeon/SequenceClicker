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
        public static DoubleAnimation FadeOpacity(this UIElement element, float value, float duration = 0.15f)
        {
            var animation = new DoubleAnimation
            {
                From = element.Opacity,
                To = value,
                Duration = TimeSpan.FromSeconds(duration),
                FillBehavior = FillBehavior.Stop
            };

            animation.OnComplete(() => element.Opacity = value);

            element.BeginAnimation(UIElement.OpacityProperty, animation);

            return animation;
        }

        public static void OnComplete(this DoubleAnimation animation, Action action)
        {
            animation.Completed += (o, e) => action();
        }
    }
}
