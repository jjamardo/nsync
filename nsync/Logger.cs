using System;
using System.Reflection;

namespace nsync
{
    class Logger
    {
        public static void Title()
        {
            string version = "v" + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            Console.Title = "NSync " + version;
            var length = " _  _ ___              ".Length;
            var last = (new string(' ', length - version.Length)) + version;
            Console.WriteLine(@" _  _ ___              ");
            Console.WriteLine(@"| \| / __|_  _ _ _  __ ");
            Console.WriteLine(@"| .` \__ \ || | ' \/ _|");
            Console.WriteLine(@"|_|\_|___/\_, |_||_\__|");
            Console.WriteLine(@"          |__/         ");
            Console.WriteLine(last);
        }
        public static void Info(string s, params string[] args)
        {
            Log(s, ConsoleColor.Green, "info", args);
        }

        public static void Warn(string s, params string[] args)
        {
            Log(s, ConsoleColor.Yellow, "warning", args);
        }

        public static void Error(string s, params string[] args)
        {
            Log(s, ConsoleColor.Red, "error", args);
        }

        public static void InfoLine(string s, params string[] args)
        {
            Log(s + "\n", ConsoleColor.Green, "info", args);
        }

        public static void WarnLine(string s, params string[] args)
        {
            Log(s + "\n", ConsoleColor.Yellow, "warning", args);
        }

        public static void ErrorLine(string s, params string[] args)
        {
            Log(s + "\n", ConsoleColor.Red, "error", args);
        }

        public static void Result(bool ok)
        {
            var color = ok ? ConsoleColor.Green : ConsoleColor.Red;
            var res = ok ? "OK" : "ERROR";
            Console.SetCursorPosition(Console.WindowWidth - res.Length - 4, Console.CursorTop);
            Console.Write("[");
            Console.ForegroundColor = color;
            Console.Write(res);
            Console.ResetColor();
            Console.WriteLine("]");
        }

        private static void Log(string s, ConsoleColor color, string type, params string[] args)
        {
            Console.Write("[{0}] ", DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss.ff"));
            Console.ForegroundColor = color;
            Console.Write(type);
            Console.ResetColor();
            Console.Write(" | " + s, args);
        }
    }
}
