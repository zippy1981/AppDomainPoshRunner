using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Reflection;
using System.Runtime.Remoting;

namespace JustAProgrammer.ADPR
{
    public sealed class AppDomainPoshRunner : MarshalByRefObject
    {
        public AppDomainPoshRunner ()
        {
            Console.WriteLine("Made it here.");
        }

        public static string[] RunScriptInAppDomain(string fileName, string appDomainName = "Unamed")
        {
            var assembly = Assembly.GetExecutingAssembly();
            var appDomain = AppDomain.CreateDomain(string.Format("AppDomainPoshRunner-{0}", appDomainName));
            try
            {
                var runner = appDomain.CreateInstanceFromAndUnwrap(assembly.Location, typeof(AppDomainPoshRunner).FullName);
                if (RemotingServices.IsTransparentProxy(runner))
                    Console.WriteLine("The unwrapped object is a proxy.");
                else
                    Console.WriteLine("The unwrapped object is not a proxy!");  
                Console.WriteLine("The unwrapped project is a {0}", runner.GetType().FullName);
                return ((AppDomainPoshRunner)runner).RunScript(fileName);
            }
            finally
            {
                AppDomain.Unload(appDomain);
            }
        }

        /// <summary>
        /// Creates a PowerShell runspace to run the script under.
        /// </summary>
        /// <param name="fileName">The name of the text file to run as a powershell script.</param>
        /// <exception cref="FileNotFoundException">Thrown if the <paramref name="fileName"/> does not exist.</exception>
        /// <returns></returns>
        private string[] RunScript(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("Powershell script not found", fileName);
            }

            var scriptText = File.ReadAllText(fileName);
            Collection<PSObject> results;
            using (var runspace = RunspaceFactory.CreateRunspace())
            {
                runspace.Open();
                using (var pipeline = runspace.CreatePipeline())
                {
                    pipeline.Commands.AddScript(scriptText);
                    pipeline.Commands.Add("Out-String");
                    results = pipeline.Invoke();
                    runspace.Close();
                }
            }


            return (from result in results select result.ToString()).ToArray();
        }
    }
}
