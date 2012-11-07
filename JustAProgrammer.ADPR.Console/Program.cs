using System;

namespace JustAProgrammer.ADPR.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1) {
                PrintUsage();
            }
            var results =  AppDomainPoshRunner.RunScriptInNewAppDomain(args[0]);
            foreach (var result in results)
            {
                System.Console.WriteLine(result);
            }
        }

        static void PrintUsage()
        {
            System.Console.WriteLine("Usage:");
            System.Console.WriteLine("\tPoshRunner.exe SCRIPT");
        }
    }
}
