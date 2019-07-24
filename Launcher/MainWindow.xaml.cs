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
        private SettingWindow settingWindow = new SettingWindow() { Visibility = Visibility.Hidden };

        private ShortcutData shortcutData = ShortcutData.Instance;

        public MainWindow()
        {
            InitializeComponent();

            mock_data();
        }

        private void mock_data()
        {
            CandidateItem item;

            item = new CandidateItem() { Keyword = "abondom" };
            shortcutData.Add(item);

            item = new CandidateItem() { Keyword = "amp" };
            shortcutData.Add(item);

            item = new CandidateItem() { Keyword = "archer" };
            shortcutData.Add(item);

            item = new CandidateItem() { Keyword = "banana" };
            shortcutData.Add(item);

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

            shortcutData.StartWith(keyword).ForEach(x => CandidateList.Items.Add(x));

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

        private void Decide() {
            if (CandidateList.Items.Count == 0) {
                Hide();
                return;
            }

            var view = CandidateList.SelectedItem as CandidateItemView;

            if (view == null) {
                Hide();
                return;
            }


            Util.Execute(view.Item.Filepath,view.Item.Application);
            Hide();
        }

        private void Keyword_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key) {
                case Key.Enter:
                    Decide();
                    break;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F1:
                    if (settingWindow.Visibility != Visibility.Visible) {
                        settingWindow.Visibility = Visibility.Visible;
                    }
                    break;
            }
        }

        private void Window_PreviewDragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;
        }

        private void Window_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            var files = e.Data.GetData(DataFormats.FileDrop) as string[];

            settingWindow.ShowShortcutAddition(file:files[0]);
            if (settingWindow.Visibility != Visibility.Visible) {
                settingWindow.Visibility = Visibility.Visible;
            }
        }
    }
}
