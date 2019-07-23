using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Launcher.Model;

namespace Launcher.View
{
    /// <summary>
    /// CandidateItemView.xaml の相互作用ロジック
    /// </summary>
    public partial class CandidateItemView : UserControl
    {
        public CandidateItem Item { get; set; } = new CandidateItem();

        public CandidateItemView()
        {
            InitializeComponent();
        }

        public CandidateItemView(CandidateItem item) : this() {
            Item = item;
            ShowData();
        }

        public void ShowData() {
            Keyword.Content = Item.Keyword;
            FilePass.Content = Item.Filepath;
        }



    }
}
