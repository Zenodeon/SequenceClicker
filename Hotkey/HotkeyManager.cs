using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SequenceClicker.Hotkey.Core;

namespace SequenceClicker.Hotkey
{
    public class HotkeyManager
    {
        public static HotkeyManager _instance;

        private KeyboardListener keyListener = new KeyboardListener();

        public static HotkeyManager Instantiate()
        {
            if (_instance == null)
            {
                _instance = new HotkeyManager();
                _instance.keyListener.KeyStateChange += new RawKeyEventHandler(_instance.ListenForHotKeys);
            }

            return _instance;
        }

        private void ListenForHotKeys(object o, RawKeyEventArgs e)
        {

        }

        public void Stop()
        {
            keyListener.Dispose();
        }
    }
}
