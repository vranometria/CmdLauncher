using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Diagnostics;
using Launcher.Model;
using Launcher.View;
using System.Windows.Input;
using Codeplex.Data;

using Launcher.Hotkey;

namespace Launcher
{
    public static class Util
    {
        public static string ReadFileToEnd(string filepath, Encoding encoding)
        {
            using (var reader = new StreamReader(filepath, encoding))
            {
                return reader.ReadToEnd();
            }
        }

        public static dynamic ReadJsonFile(string filepath, Encoding encoding)
        {
            var s = ReadFileToEnd(filepath, encoding);

            if(string.IsNullOrEmpty(s))
            {
                return null;
            }

            return DynamicJson.Parse(s);
        }
            
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
                    if (IsNotExist(file: file)) { return null; }
                    return Process.Start(f);

                case 2:
                    if (IsNotExist(app: app)) { return null; }
                    return Process.Start(a);

                case 3:
                    if (IsNotExist(file: file, app: app)) { return null; }
                    return Process.Start(a, f);

            }

            throw new Exception();
        }

        public static bool IsNotExist(string file = null , string app = null )
        {
            if (file != null)
            { 
                if (File.Exists(file))
                {
                    return false;
                }

                if (Directory.Exists(file))
                {
                    return false;
                }
            }

            if (app != null)
            {
                if (File.Exists(file))
                {
                    return false;
                }
            }

            return true;
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
