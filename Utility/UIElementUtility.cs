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
        public static void FadeOpacity(this UIElement element, float value)
        {
            var animation = new DoubleAnimation
            {
                From = element.Opacity,
                To = value,
                Duration = TimeSpan.FromSeconds(0.15f),
                FillBehavior = FillBehavior.Stop
            };

            animation.Completed += (o, e) => element.Opacity = value;

            element.BeginAnimation(UIElement.OpacityProperty, animation);
        }
    }
}
