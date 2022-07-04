using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceClicker.Component
{
    public interface IMenuPanelContent
    {
        void AddMenuContent(ref List<MenuButtonBP> buttons);
    }
}
