using System;
using System.IO;
using System.Threading;

namespace nsync
{
    class Program
    {
        private static Watcher watcher = null;
        private static Synchronizer synchro = null;
        private static bool running = true;
        private static string targetPath = null;

        public static void Main(string[] args)
        {
            Logger.Title();
            var config = new Config();
            var argsHelper = new ArgsHelper(args);

            targetPath = config.Target;

            /* Initializing WinSCP synchronization */
            synchro = new Synchronizer();
            synchro.Connect(config.Host, config.Port, config.User, config.Pass, config.Fingerprint);
            InitialSynchronization(config, argsHelper);

            /* Initializing FileSystem watcher */
            watcher = new Watcher();
            watcher.Watch(config.Source, OnChange);

            Console.CancelKeyPress += new ConsoleCancelEventHandler(CtrlCHandler);
            while (running)
            {
                Thread.Sleep(1000);
            }

            watcher.Stop();
            synchro.Close();
        }
        private static void OnChange(string name, string fullPath)
        {
            var destPath = Path.Combine(targetPath, name).Replace('\\', '/');
            synchro.Sync(fullPath, destPath);
        }

        private static void InitialSynchronization(Config config, ArgsHelper argsHelper)
        {
            bool initialSync = false;
            if (argsHelper.InitialSyncSeted)
            {
                /* If the argument is present, overrides the value in the configuration file */
                if (argsHelper.InitialSync)
                {
                    initialSync = true;
                }
            }
            else if (config.InitialSync)
            {
                initialSync = true;
            }

            if (initialSync)
            {
                Logger.InfoLine("Initial synchronization is enabled. Synchronizing all files...");
                var parent = config.Target.Substring(0, targetPath.LastIndexOf(Directory.GetParent(targetPath).Name));
                synchro.Sync(config.Source, parent);
                Logger.InfoLine("Initial synchronization finished");
            }
            else
            {
                Logger.WarnLine("Initial synchronization is disabled");
            }
        }


        private static void CtrlCHandler(object sender, ConsoleCancelEventArgs e)
        {
            Logger.Warn("Ctrl+C pressed. Exiting...");
            e.Cancel = true;
            running = false;
        }
    }
}

