using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Collections;

namespace Launcher.Hotkey
{
    public interface FireHotkeyEventWindow
    {
        void ExecuteEvent();

        Window TargetWindow { get; }
    }
}
