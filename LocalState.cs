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
        public static MainWindow MainWindow { get; set; }
        public static OverlayWindow OverlayWindow { get; set; }

        public static MenuPanel MenuPanel { get; set; }
    }
}
