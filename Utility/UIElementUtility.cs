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
        public static ElementPropertyHandler FadeProperty(this UIElement element, DependencyProperty dp, double value, int msDuration = 150)
        {
            ElementPropertyHandler handler = new ElementPropertyHandler();

            DoubleAnimation animation = new DoubleAnimation
            {
                From = (double)element.GetValue(dp),
                To = value,
                Duration = TimeSpan.FromMilliseconds(msDuration),
                FillBehavior = FillBehavior.Stop
            };

            animation.Completed += (o, e) => element.SetValue(dp, value);
           
            element.BeginAnimation(dp, animation);

            handler.animation = animation;
            return handler;
        }

        public static ElementPropertyHandler OnComplete(this ElementPropertyHandler handler, Action action)
        {
            handler.completeAction = action;
            handler.animation.Completed += handler.OnComplete;
            return handler;
        }

        public static ElementPropertyHandler OnUpdate(this ElementPropertyHandler handler, Action action)
        {
            handler.updateAction = action;
            handler.animation.CurrentTimeInvalidated += handler.OnUpdate;
            return handler;
        }

        public class ElementPropertyHandler
        {
            public DoubleAnimation animation;

            public Action updateAction;
            public Action completeAction;

            public void OnUpdate(object? obj, EventArgs e) => updateAction();
            public void OnComplete(object? obj, EventArgs e) => completeAction();
        }
    }
}
