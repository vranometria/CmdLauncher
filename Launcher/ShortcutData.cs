using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Launcher.Model;
using Launcher.View;

namespace Launcher
{
    public class ShortcutData
    {
        private static ShortcutData instance;

        public static ShortcutData Instance { get { return instance == null ? (instance = new ShortcutData()) : instance; } }

        private List<CandidateItemView> ItemViews = new List<CandidateItemView>();

        private ShortcutData() {
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
