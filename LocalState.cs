using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SequenceClicker.Component;
using SequenceClicker.View;

namespace SequenceClicker
{
    public static class LocalState
    {
        public static MainWindow mainWindow { get; set; }
        public static OverlayWindow overlayWindow { get; set; }

        public static MenuPanel menuPanel { get; set; }
    }
}
