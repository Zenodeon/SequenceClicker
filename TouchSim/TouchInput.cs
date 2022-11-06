using System;
using System.Windows;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceClicker.TouchSim
{
    public static class TouchInput
    {
        static PointerTouchInfo touchInfo;

        public static void Initialize(TouchFeedback touchFeedback = TouchFeedback.INDIRECT)
        {
            TouchInjector.InitializeTouchInjection(255, feedbackMode: touchFeedback);

            touchInfo = MakePointerTouchInfo();

            ExecuteTouchAction(TouchAction.Initialize);
        }

        public static void SetTouchPoint(int id, Point screenPos)
        {
            touchInfo.PointerInfo.PointerId = (uint)id;

            touchInfo.PointerInfo.PtPixelLocation.X = (int)screenPos.X - 7;
            touchInfo.PointerInfo.PtPixelLocation.Y = (int)screenPos.Y - 7;
        }

        public static void ExecuteTouchAction(TouchAction action)
        {
            bool actionSuccess = false;

            PointerFlags flags = action switch
            {
                TouchAction.Initialize => PointerFlags.INRANGE | PointerFlags.UPDATE,
                TouchAction.Touch => PointerFlags.INRANGE | PointerFlags.INCONTACT | PointerFlags.DOWN,
                TouchAction.Update => PointerFlags.INRANGE | PointerFlags.INCONTACT | PointerFlags.UPDATE,
                TouchAction.End => PointerFlags.UP,
                _ => PointerFlags.UPDATE
            };

            touchInfo.PointerInfo.PointerFlags = flags;
            actionSuccess = TouchInjector.InjectTouchInput(1, new[] { touchInfo });

            if (!actionSuccess)
                DLog.Warn("Touch Action Failed : " + action + " || Error Code : " + Marshal.GetLastWin32Error());
        }

        private static PointerTouchInfo MakePointerTouchInfo(int id = 0, uint pressure = 16000)
        {
            PointerTouchInfo info = new PointerTouchInfo();

            info.PointerInfo.PointerId = (uint)id;

            info.PointerInfo.pointerType = PointerInputType.TOUCH;

            info.TouchFlags = TouchFlags.NONE;

            info.ContactArea.top = info.PointerInfo.PtPixelLocation.Y + 2;
            info.ContactArea.bottom = info.PointerInfo.PtPixelLocation.Y - 2;
            info.ContactArea.right = info.PointerInfo.PtPixelLocation.X + 2;
            info.ContactArea.left = info.PointerInfo.PtPixelLocation.X - 2;

            info.TouchMasks = TouchMask.PRESSURE;
            info.Pressure = pressure;

            return info;
        }

        public enum TouchAction
        {
            Initialize,
            Touch,
            Update,
            End
        }
    }
}
