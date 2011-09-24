using System;
using System.Reflection;

namespace JustAProgrammer.ADPR.Helper
{
    public static class AppDomainPoshRunnerHelper
    {
        public static string[] RunScriptInAppDomain(string fileName, string appDomainName = "Unamed")
        {
            var assembly = typeof(AppDomainPoshRunner).Assembly;

            var setupInfo = new AppDomainSetup
                                {
                                    ApplicationName = appDomainName,
                                    // TODO: Perhaps we should setup an even handler to reload the AppDomain similar to ASP.NET in IIS.
                                    ShadowCopyFiles = "true"
                                };
            var appDomain = AppDomain.CreateDomain(string.Format("AppDomainPoshRunner-{0}", appDomainName), null,
                                                   setupInfo);
            try
            {
                var runner = appDomain.CreateInstanceFromAndUnwrap(assembly.Location,
                                                                   typeof (AppDomainPoshRunner).FullName);
                Console.WriteLine("The unwrapped project is a {0}", runner.GetType().FullName);
                return ((AppDomainPoshRunner) runner).RunScript(fileName);
            }
            finally
            {
                AppDomain.Unload(appDomain);
            }
        }
    }
}