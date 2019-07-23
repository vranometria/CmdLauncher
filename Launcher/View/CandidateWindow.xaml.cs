using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Launcher.Model;

namespace Launcher.View
{
    /// <summary>
    /// CandidateWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class CandidateWindow : Window
    {
        private List<CandidateItemView> candidateItemViews = new List<CandidateItemView>();

        public CandidateWindow()
        {
            InitializeComponent();

            mock_data();
        }

        private void mock_data() {
            CandidateItemView view;
            List<CandidateItemView> views = new List<CandidateItemView>();

            view = new CandidateItemView( new CandidateItem() { Keyword = "abondom" });
            views.Add(view);

            view = new CandidateItemView(new CandidateItem() { Keyword = "amp" });
            views.Add(view);

            view = new CandidateItemView(new CandidateItem() { Keyword = "archer" });
            views.Add(view);

            view = new CandidateItemView(new CandidateItem() { Keyword = "banana" });
            views.Add(view);

            candidateItemViews = views;
        }

        internal void ShowCandidate(string keyword)
        {
            CandidateList.Items.Clear();

            candidateItemViews.Where(x => x.Item.Keyword.StartsWith(keyword)).ToList().ForEach(x => CandidateList.Items.Add(x));
        }
    }
}
