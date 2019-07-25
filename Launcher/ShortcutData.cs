using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Launcher.Model;
using Launcher.View;
using System.IO;
using Codeplex.Data;

namespace Launcher
{
    public class ShortcutData
    {
        private const string DATA_FILE = "data.json";

        private static ShortcutData instance;

        public static ShortcutData Instance { get { return instance == null ? (instance = new ShortcutData()) : instance; } }

        private List<CandidateItemView> ItemViews = new List<CandidateItemView>();

        private ShortcutData() {
            Load();
        }

        private void Load() {
            if (!File.Exists(DATA_FILE)) {
                using (var writer = new StreamWriter(DATA_FILE))
                {
                    writer.WriteLine("{\"data\":[]}");
                }
            }

            using (StreamReader reader = new StreamReader(DATA_FILE)) {
                string s = reader.ReadToEnd();
                var obj = DynamicJson.Parse(s);
                ItemViews = Util.ConvertData(obj);
            }
        }

        public void Save() {

            var obj = new { data = ItemViews.Select(x => x.Item).ToList() };

            using (var writer = new StreamWriter(DATA_FILE)) {
                writer.WriteLine(DynamicJson.Serialize(obj));
            }
        }

        public void Add(CandidateItem item) {
            ItemViews.Add(new CandidateItemView(item));
        }

        public void Add(string key, string file, string app) {
            Add(new CandidateItem() { Keyword = key, Filepath = file, Application = app });
           
        }

        public void Add(IEnumerable<CandidateItem> items) {
            items.ToList().ForEach(x => Add(x));
        }

        public List<CandidateItemView> StartWith(string key) {
            return ItemViews.Where(x => x.Item.Keyword.StartsWith(key)).ToList();
        }

    }
}
