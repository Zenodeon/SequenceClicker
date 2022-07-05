using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceClicker.Component
{
    public struct MenuButtonBP
    {
        public string name;
        public Action callback;

        public ButtonState callbackOnState;

        public MenuButtonBP(string name, Action callback, ButtonState callbackOnState)
        {
            this.name = name;
            this.callback = callback;
            this.callbackOnState = callbackOnState;
        }
    }

    public enum ButtonState
    {
        Down,
        Drag,
        Up
    }
}
