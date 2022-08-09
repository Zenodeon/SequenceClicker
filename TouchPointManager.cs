using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using SequenceClicker.API;
using SequenceClicker.TouchSim;

namespace SequenceClicker
{
    public static class TouchPointManager
    {
        private static void TouchAtPoint(int id, Point screenPoint)
        {
            IntPtr targetWindow = User32API.WindowFromPoint(new User32API.POINT(screenPoint));
            User32API.SetForegroundWindow(targetWindow);

            TouchInput.SetTouchPoint(0, screenPoint);

            TouchInput.ExecuteTouchAction(TouchInput.TouchAction.Touch);

            Thread.Sleep(1);

            int timer = 0;

            while (timer != 30)
            {
                TouchInput.ExecuteTouchAction(TouchInput.TouchAction.Update);

                timer++;
                Thread.Sleep(1);
            }

            TouchInput.ExecuteTouchAction(TouchInput.TouchAction.End);
        }
    }
}
