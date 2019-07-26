﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using Launcher.Hotkey;
using Launcher.View;
using Launcher.Model;
using System.Collections;

namespace Launcher
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window , FireHotkeyEventWindow
    {
        private SettingWindow settingWindow;

        private ShortcutData shortcutData = ShortcutData.Instance;

        private AppConfig config = AppConfig.Instance;

        public Window TargetWindow => this;

        public LauncherHotkey Hotkey { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            settingWindow = new SettingWindow(this) { Visibility = Visibility.Hidden };

            Hotkey = new LauncherHotkey(this);

            RegisterHotkey();
        }

        private void Keyword_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = Keyword.Text;
            ShowCandidate(keyword);
        }

        private void RegisterHotkey() {
            if (string.IsNullOrWhiteSpace(config.Hotkey)) {
                return;
            }

            Key key = Util.Parse(config.Hotkey);

            Util.RegisterHotkey(Hotkey, key, config.ModifierAlt, config.ModifierControl);
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            shortcutData.Save();

            config.Save();

            Hotkey.Unregister();

            settingWindow.Close();
        }

        public void ExecuteEvent()
        {
            Activate();
        }
    }
}
