using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceClicker.Component
{
    public struct MenuButtonBP
    {
        public string name { get; set; }
        public Action callback { get; set; }

        public MenuButtonBP(string name, Action callback)
        {
            this.name = name;
            this.callback = callback;
        }
    }
}
