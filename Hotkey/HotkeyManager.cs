using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

using SequenceClicker.Hotkey.Core;

namespace SequenceClicker.Hotkey
{
    public class HotkeyManager
    {
        public static HotkeyManager _instance;

        private KeyboardListener keyListener = new KeyboardListener();

        private Dictionary<Key, Action<Hotkey>> hotkeys = new Dictionary<Key, Action<Hotkey>>();

        private List<Key> hotkeysHeldDown = new List<Key>();

        public static HotkeyManager Instantiate()
        {
            if (_instance == null)
            {
                _instance = new HotkeyManager();
                _instance.keyListener.KeyStateChange += new RawKeyEventHandler(_instance.ListenForHotKeys);
            }

            return _instance;
        }

        public void RegisterKey(Key key, Action<Hotkey> callback)
        {
            if (!hotkeys.ContainsKey(key))
                hotkeys.Add(key, callback);
            else
                DLog.Warn($"Registering Hotkey Failed - Hotkey already exists for Key : {key}");
        }

        public void DeregisterKey(Key key)
        {
            hotkeys.Remove(key);
            hotkeysHeldDown.Remove(key);
        }

        private void ListenForHotKeys(object o, RawKeyEventArgs e)
        {
            if (!hotkeys.ContainsKey(e.Key))
                return;

            if (hotkeysHeldDown.Contains(e.Key))
            {
                if (!e.isKeyDown)
                    hotkeysHeldDown.Remove(e.Key);
                else
                    return;
            }
            else
                hotkeys[e.Key]?.Invoke(new Hotkey(e.Key));
        }

        public void Stop()
        {
            keyListener.Dispose();
        }
    }

    public struct Hotkey
    {
        public Key key;

        public Hotkey(Key key)
        {
            this.key = key;
        }
    }
}
