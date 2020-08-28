using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Annotations;

namespace Launcher
{
    public class ReservedKey
    {
        public static readonly string[] WORDS = new[] { "exit" };

        public MainWindow MainWindow { get; private set; }


        public static bool IsMatch(string key) => WORDS.Contains(key);


        public ReservedKey(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
        }

        public void Do(string key)
        {
            switch(key)
            {
                case "exit":
                    MainWindow.Close();
                    break;
            }
        }
    }
}
