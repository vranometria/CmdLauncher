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
using Launcher.View;
using Launcher.Model;

namespace Launcher
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<CandidateItemView> candidateItemViews = new List<CandidateItemView>();

        public MainWindow()
        {
            InitializeComponent();

            mock_data();
        }

        private void mock_data()
        {
            CandidateItemView view;
            List<CandidateItemView> views = new List<CandidateItemView>();

            view = new CandidateItemView(new CandidateItem() { Keyword = "abondom" });
            views.Add(view);

            view = new CandidateItemView(new CandidateItem() { Keyword = "amp" });
            views.Add(view);

            view = new CandidateItemView(new CandidateItem() { Keyword = "archer" });
            views.Add(view);

            view = new CandidateItemView(new CandidateItem() { Keyword = "banana" });
            views.Add(view);

            candidateItemViews = views;
        }

        private void Keyword_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = Keyword.Text;
            ShowCandidate(keyword);
        }

        internal void ShowCandidate(string keyword)
        {
            CandidateList.Items.Clear();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                CandidateViewArea.Visibility = Visibility.Hidden;
                return;
            }

            candidateItemViews.Where(x => x.Item.Keyword.StartsWith(keyword)).ToList().ForEach(x => CandidateList.Items.Add(x));

            if (CandidateList.Items.Count == 0)
            {
                CandidateViewArea.Visibility = Visibility.Hidden;
            }
            else
            {
                CandidateList.SelectedIndex = 0;
                CandidateViewArea.Visibility = Visibility.Visible;
            }
        }

        private void Keyword_KeyDown(object sender, KeyEventArgs e)
        {
        }

    }
}
