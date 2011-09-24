namespace JustAProgrammer.ADPR.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var results =  AppDomainPoshRunner.RunScriptInAppDomain("AppDomainPoshRunner.SecondAppDomain.ps1");
            foreach (var result in results)
            {
                System.Console.WriteLine(result);
            }
            System.Console.Write("Press Any Key To Continue . . . ");
            System.Console.ReadKey(true);
            System.Console.WriteLine();
        }
    }
}
