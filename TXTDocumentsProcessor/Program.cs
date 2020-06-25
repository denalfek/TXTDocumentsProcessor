using System;
using TXTDocumentsProcessor.Services;

namespace TXTDocumentsProcessor
{
    class Program
    {
        static void Main()
        {
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length != 3)
            {
                Console.WriteLine("Need 2 args: source folder path and destionation folder path");
                return;
            }

            Console.WriteLine("Listening...");
            WatcherService.Run(args[1]);

            var key = Console.ReadLine();
            if (key == "q") Console.WriteLine("Bye");
        }
    }
}
