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
    public static class WPFUtility
    {
        public static void LoseFocus(this FrameworkElement element)
        {
            Keyboard.ClearFocus();
            FocusManager.SetFocusedElement(FocusManager.GetFocusScope(element), null);
        }
    }
}
