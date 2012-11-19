using System;
using System.Diagnostics;

namespace JustAProgrammer.ADPR.Console
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 1) {
                PrintUsage();
                return 2;
            }
            var results =  AppDomainPoshRunner.RunScriptInNewAppDomain(new ADPRConfig(args[0]));
            foreach (var result in results)
            {
                System.Console.WriteLine(result);
            }
            // TODO: Get the state from the PSHost
            return 0;
        }

        static void PrintUsage()
        {
            System.Console.WriteLine("Usage:");
            System.Console.WriteLine("\tPoshRunner.exe [[drive:\\]path\\to\\]script");
        }
    }
}
