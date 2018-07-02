using System;
using Take.Blip.Client.Console;

namespace TaskBot
{
    class Program
    {
        static int Main(string[] args) => ConsoleRunner.RunAsync(args).GetAwaiter().GetResult();
    }
}