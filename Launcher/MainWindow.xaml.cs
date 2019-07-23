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
using System.Windows.Navigation;
using Launcher.View;

namespace Launcher
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private CandidateWindow candidateWindow = new CandidateWindow();

        public MainWindow()
        {
            InitializeComponent();

            candidateWindow.Left = Left + Width;
            candidateWindow.Top = Top;
            candidateWindow.Visibility = Visibility.Visible;
        }

        private void Keyword_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = Keyword.Text;
            candidateWindow.ShowCandidate(keyword);
        }
    }
}
