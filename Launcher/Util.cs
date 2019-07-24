using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Launcher
{
    public static class Util
    {
        public static Process Execute(string file = null, string app = null) {

            int code = 0;

            if (!string.IsNullOrEmpty(file)) {
                code += 1;
            }

            if (!string.IsNullOrEmpty(app)) {
                code += 2;
            }

            var f = $"\"{file.Replace("\"","")}\"";
            var a = $"\"{app.Replace("\"", "")}\"";

            switch (code) {

                case 1:
                    return Process.Start(f);

                case 2:
                    return Process.Start(a);

                case 3:
                    return Process.Start(a, f);

            }

            throw new Exception();

        }
    }
}
