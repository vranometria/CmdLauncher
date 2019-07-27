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
using Launcher.Model;

namespace Launcher
{
    /// <summary>
    /// SettingWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingWindow : Window
    {
        private ShortcutData shortcutData = ShortcutData.Instance;

        private AppConfig appConfig = AppConfig.Instance;

        private MainWindow mainWindow;

        public SettingWindow()
        {
            InitializeComponent();
        }

        public SettingWindow(MainWindow mainWindow) : this() {
            this.mainWindow = mainWindow;

            Init();
        }

        private void Init() {
            AppConfig config = AppConfig.Instance;
            if (config.Hotkey != null) {
                Key key = (Key) Enum.Parse(typeof(Key), config.Hotkey);
                HotkeySelector.Text = key.ToString();
                HotkeySelector.Tag = key;
                ModkeyAlt.IsChecked = config.ModifierAlt;
                ModkeyCtrl.IsChecked = config.ModifierControl;
            }
            
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
            appConfig.Save();

            shortcutData.Save();

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

            var item = shortcutData.Match(key).FirstOrDefault();
            if (item != null) {
                shortcutData.Delete(key);
            }

            shortcutData.Add(key,file,app);

            ClearShortcutAddTab();
        }

        internal void ShowShortcutAddition(string file = null, string app = null)
        {
            ShortcutTargetFile.Text = file;
            ShortcutTagetApp.Text = app;
            OpenShortcutTab();
        }

        /// <summary>
        /// ショートカット・キーワードが登録済みではない時
        /// </summary>
        private void NoRregisteredShortcut()
        {
            DeleteButton.IsEnabled = false;
            ShortcutDataShowButton.IsEnabled = false;
            ExistIndicator.Content = null;
            ShortcutAdder.Content = "Add";
        }

        /// <summary>
        /// ショートカット・キーワードが登録済みである時
        /// </summary>
        private void ExistReigsterdShortcut() {
            DeleteButton.IsEnabled = true;
            ShortcutDataShowButton.IsEnabled = true;
            ShortcutAdder.Content = "Overwrite";
            ExistIndicator.Content = "already exist!";
        }

        private void ClearShortcutTab()
        {
            ShortcutKeyword.Text = null;
            ShortcutTargetFile.Text = null;
            ShortcutTagetApp.Text = null;

        }

        private void HotkeySelector_KeyDown(object sender, KeyEventArgs e)
        {
            HotkeySelector.Text = e.Key.ToString();
            HotkeySelector.Tag = e.Key;
        }

        private void HotkeyApplyer_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(HotkeySelector.Text)) {
                return;
            }

            Key key = (Key) HotkeySelector.Tag;
            bool alt = ModkeyAlt.IsChecked == true;
            bool ctrl = ModkeyCtrl.IsChecked == true;


            var success = Util.RegisterHotkey(mainWindow.Hotkey, key, alt, ctrl);
            var msg = success ? "register hotkey!" : "fail to register hotkey";
            HotkeyResisterResult.Content = msg;

            if (success) {
                appConfig.Hotkey = key.ToString();
                appConfig.ModifierAlt = (bool)ModkeyAlt.IsChecked;
                appConfig.ModifierControl = (bool)ModkeyCtrl.IsChecked;
            }
        }

        private void ShortcutKeyword_TextChanged(object sender, TextChangedEventArgs e)
        {
            string key = ShortcutKeyword.Text;

            CandidateItem item = shortcutData.Match(key).FirstOrDefault()?.Item;
            if (item == null)
            {
                NoRregisteredShortcut();
                return;
            }

            ExistReigsterdShortcut();
        }

        private void ShortcutDataShowButton_Click(object sender, RoutedEventArgs e)
        {
            string key = ShortcutKeyword.Text;
            CandidateItem item = shortcutData.Match(key).FirstOrDefault()?.Item;

            ShortcutTargetFile.Text = item.Filepath;
            ShortcutTagetApp.Text = item.Application;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            string key = ShortcutKeyword.Text;
            shortcutData.Delete(key);
            ClearShortcutAddTab();
        }
    }
}
