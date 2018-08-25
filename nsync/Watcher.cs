using System;
using System.Collections.Generic;
using System.IO;

namespace nsync
{
    class Watcher
    {
        private FileSystemWatcher watcher = new FileSystemWatcher();
        public delegate void WatcherHandler(string name, string fullPath);
        public event WatcherHandler OnChanged;
        private Dictionary<string, DateTime> lastChange = new Dictionary<string, DateTime>();

        public void Watch(string sourcePath, WatcherHandler handler)
        {
            OnChanged = handler;
            watcher.IncludeSubdirectories = true;
            watcher.Path = sourcePath;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Changed += new FileSystemEventHandler(OnFileSystemChanged);
            watcher.EnableRaisingEvents = true;
        }

        private void OnFileSystemChanged(object sender, FileSystemEventArgs e)
        {
            DateTime dt = File.GetLastWriteTime(e.FullPath);
            if (lastChange.ContainsKey(e.FullPath))
            {
                if (dt <= lastChange[e.FullPath])
                {
                    return;
                }
                lastChange[e.FullPath] = dt;
            }
            else
            {
                lastChange.Add(e.FullPath, dt);
            }
            OnChanged?.Invoke(e.Name, e.FullPath);
        }

        public void Stop()
        {
            watcher.EnableRaisingEvents = false;
            watcher.Changed -= OnFileSystemChanged;
            watcher.Dispose();
            watcher = null;
        }
    }
}
