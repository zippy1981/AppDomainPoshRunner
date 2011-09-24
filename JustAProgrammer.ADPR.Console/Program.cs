using JustAProgrammer.ADPR.Helper;

namespace JustAProgrammer.ADPR.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var results = AppDomainPoshRunnerHelper.RunScriptInAppDomain("AppDomainPoshRunner.SecondAppDomain.ps1");
            foreach (var result in results)
            {
                System.Console.WriteLine(result);
            }
        }
    }
}
