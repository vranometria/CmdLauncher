using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Launcher.Model;
using Launcher.View;
using System.Windows.Input;
using Launcher.Hotkey;

namespace Launcher
{
    public static class Util
    {
        public static Process Execute(string file = null, string app = null) {

            int code = 0;

            if (!string.IsNullOrEmpty(file)) {
                code += 1;
            }

            if (!string.IsNullOrEmpty(app)) {
                code += 2;
            }

            var f = $"\"{file.Replace("\"","")}\"";
            var a = $"\"{app.Replace("\"", "")}\"";

            switch (code) {

                case 1:
                    return Process.Start(f);

                case 2:
                    return Process.Start(a);

                case 3:
                    return Process.Start(a, f);

            }

            throw new Exception();

        }

        public static List<CandidateItemView> ConvertData(dynamic readData) {
            List<CandidateItemView> views = new List<CandidateItemView>();
            foreach (dynamic record in readData.data) {
                var model = new CandidateItem()
                {
                    Keyword = record.Keyword,
                    Filepath = record.Filepath,
                    Application = record.Application
                };
                views.Add(new CandidateItemView(model));
            }
            return views;
        }

        public static bool RegisterHotkey(LauncherHotkey hotkey, Key key, bool alt, bool ctrl) {

            ModifierKeys modifierKey = ModifierKeys.None;

            if (alt)
            {
                modifierKey = modifierKey == ModifierKeys.None ? ModifierKeys.Alt : modifierKey | ModifierKeys.Alt;
            }

            if (ctrl)
            {
                modifierKey = modifierKey == ModifierKeys.None ? ModifierKeys.Control : modifierKey | ModifierKeys.Control;
            }

            return hotkey.Register(modifierKey, key);
        }

        public static Key Parse(string key) {
            if (string.IsNullOrEmpty(key)) {
                return Key.None;
            }

            return (Key)Enum.Parse(typeof(Key), key);
        }
    }
}
