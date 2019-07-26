using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Input;

namespace Launcher.Hotkey
{
    public class LauncherHotkey : IDisposable
    {
        private const int WM_HOTKEY = 0x0312;

        private const int MAX_HOTKEY_ID = 0xC000;

        [DllImport("user32.dll")]
        private static extern int RegisterHotKey(IntPtr hWnd, int id, int modkey, int key);

        [DllImport("user32.dll")]
        private static extern int UnregisterHotKey(IntPtr hWnd, int id);

        private IntPtr windowHandle;

        private int hotkeyId;

        FireHotkeyEventWindow target;

        public LauncherHotkey(FireHotkeyEventWindow obj) {
            target = obj;
            windowHandle = (new WindowInteropHelper(obj.TargetWindow)).Handle;
            ComponentDispatcher.ThreadPreprocessMessage += delegate(ref MSG msg, ref bool handled){
                if (msg.message != WM_HOTKEY) {
                    return;
                }
                var id = msg.wParam.ToInt32();

                if (id == hotkeyId) {
                    target.ExecuteEvent();
                }
            };
        }

        public bool Register(ModifierKeys modifierKey, Key key) {

            int modkey = (int)modifierKey;
            int virtualKey = KeyInterop.VirtualKeyFromKey(key);
            int hotkeyId = 0x0000;

            while (hotkeyId < MAX_HOTKEY_ID) {
                var success = RegisterHotKey(windowHandle, hotkeyId, modkey, virtualKey) != 0;
                if (success) {
                    this.hotkeyId = hotkeyId;
                    return true;
                }
                hotkeyId++;
            }

            return false;
        }

        public bool Unregister(){
            return UnregisterHotKey(windowHandle, hotkeyId) == 0;
        }


        public void Dispose()
        {
            Unregister();
        }
    }
}
