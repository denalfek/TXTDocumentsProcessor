using System;
using System.IO;
using System.Linq;
using static System.Console;
using static System.ConsoleColor;

namespace TXTDocumentsProcessor.Services
{
    public static class WatcherService
    {
        public static void Run()
        {
            string[] args = Environment.GetCommandLineArgs();
            string path = args[1];

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

            CountChars(e.FullPath, e.Name);
            CopyProcessedFile(e.FullPath, e.Name);
        }

        private static void CountChars(string filePath, string fileName)
        {            
            var charsCount = File.ReadAllText(filePath).Length;
            var processedValuesPath = @"C:\processedValues\";
            var isExists = Directory.Exists(processedValuesPath);

            if (!isExists)
            {
                return;
            }

            using var writer = new StreamWriter(processedValuesPath + fileName);
            writer.Write(charsCount);
        }

        private static void CopyProcessedFile(string filePath, string fileName)
        {
            try
            {
                string[] args = Environment.GetCommandLineArgs();
                var path = args[2];
                var destPath = path + @"\" + fileName;
                File.Copy(filePath, destPath);
            }
            catch(Exception ex)
            {
                WriteLine(ex.Message);
            }
        }

        #endregion
    }
}
