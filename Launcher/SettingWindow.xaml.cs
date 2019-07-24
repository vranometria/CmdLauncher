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
using System.Windows.Shapes;

namespace Launcher
{
    /// <summary>
    /// SettingWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingWindow : Window
    {
        private ShortcutData shortcutData = ShortcutData.Instance;

        public SettingWindow()
        {
            InitializeComponent();
        }

        private void OpenShortcutTab() {
            Tab.SelectedIndex = 1;
        }

        private void ClearShortcutAddTab() {
            ShortcutKeyword.Text = null;
            ShortcutTargetFile.Text = null;
            ShortcutTagetApp.Text = null;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void ShortcutAdder_Click(object sender, RoutedEventArgs e)
        {
            var key = ShortcutKeyword.Text;
            if (string.IsNullOrWhiteSpace(key)) {
                return;
            }

            var file = ShortcutTargetFile.Text.Trim();
            var app = ShortcutTagetApp.Text.Trim();

            shortcutData.Add(key,file,app);

            ClearShortcutAddTab();
        }

        internal void ShowShortcutAddition(string file = null, string app = null)
        {
            ShortcutTargetFile.Text = file;
            ShortcutTagetApp.Text = app;
            OpenShortcutTab();
        }
    }
}
