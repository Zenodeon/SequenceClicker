using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceClicker.API
{
    public static class User32DS
    {
        public const int WM_HOTKEY = 0x0312;

        public enum HKMod
        {
            MOD_NONE = 0x0,
            MOD_ALT = 0x0001,
            MOD_CONTROL = 0x0002,
            MOD_NOREPEAT = 0x4000,
            MOD_SHIFT = 0x0004,
            MOD_WIN = 0x0008
        }
    }
}
