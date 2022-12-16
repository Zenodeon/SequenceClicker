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
    public static class DoubleAnimationUtility
    {
        public static DoubleAnimation OnComplete(this DoubleAnimation animation, Action action)
        {
            animation.Completed += (o, e) => action();
            return animation;
        }

        public static DoubleAnimation OnUpdate(this DoubleAnimation animation, Action action)
        {
            animation.CurrentTimeInvalidated += (o, e) => action();
            return animation;
        }
    }
}
