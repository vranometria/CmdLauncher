using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Codeplex.Data;

namespace Launcher
{
    public class HotkeyConfig
    {
        private const string DATA_FILE = "config.json";

        private static HotkeyConfig instance;
        public static HotkeyConfig Instance { get { return instance == null ? (instance = new HotkeyConfig()) : instance; } }

        public string Hotkey { get; set; }

        public bool ModifierAlt { get; set; }

        public bool ModifierControl { get; set; }

        private HotkeyConfig() {
            if (File.Exists(DATA_FILE)) {
                Load();
            }


        }

        private void Load() {
            using (StreamReader reader = new StreamReader(DATA_FILE)) {
                dynamic json = DynamicJson.Parse(reader.ReadToEnd());
                Hotkey = json.Hotkey;
                ModifierAlt = json.ModifierAlt;
                ModifierControl = json.ModifierControl;
            }
        }


        public void Save() {
            using (StreamWriter writer = new StreamWriter(DATA_FILE)) {
                var s = DynamicJson.Serialize(this);
                writer.WriteLine(s);
            }
        }

    }
}
