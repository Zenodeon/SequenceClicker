﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceClicker.View.BasicSequencer.Component
{
    public interface IDelay
    {
        public void LiveMode(bool state);
        public DelayControl Delay(Action updateCallback, Action callback);
    }
}
