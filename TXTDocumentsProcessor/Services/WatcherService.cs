using System.IO;
using static System.Console;
using static System.ConsoleColor;

namespace TXTDocumentsProcessor.Services
{
    public static class WatcherService
    {
        public static void Run(string path)
        {
            SubscribeToEvents(
                new FileSystemWatcher
                {
                    Path = path,
                    EnableRaisingEvents = true
                });
        }

        #region private methods

        private static void SubscribeToEvents(FileSystemWatcher watcher)
        {
            watcher.Created += Watcher_Created;
            watcher.Deleted += Watcher_Deleted;
            watcher.Renamed += Watcher_Renamed;
            watcher.Changed += Watcher_Changed;
        }

        private static void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            ForegroundColor = Yellow;
            WriteLine($"File {e.Name} has been changed");
        }

        private static void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            ForegroundColor = Blue;
            WriteLine($"File has been renamed from {e.OldName} to {e.Name}");
        }

        private static void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            ForegroundColor = Red;
            WriteLine($"File {e.Name} has been deleted");
        }

        private static void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            ForegroundColor = Green;
            WriteLine($"File {e.Name} was added");
        }

        #endregion
    }
}
