using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Management.Automation.Runspaces;
using System.Reflection;

namespace JustAProgrammer.ADPR
{
    public sealed class AppDomainPoshRunner : MarshalByRefObject
    {
        /// <summary>
        /// Created a new <see cref="AppDomain"/> and runs the given powershell script.
        /// </summary>
        /// <param name="fileName">The name of the powershell script to run.</param>
        /// /// <param name="configFileName">The name of the configuration file. If you set it to null it will defaul to <code><paramref name="fileName"/>.config</code>.</param>
        /// <param name="appDomainName">The name of the AppDomain.</param>
        /// <returns>The output of the script as an array of strings.</returns>
        public static string[] RunScriptInNewAppDomain(string fileName, string configFileName = null, string appDomainName = "AppDomainPoshRunner")
        {
            var assembly = Assembly.GetExecutingAssembly();
            
            var setupInfo = new AppDomainSetup
                                {
                                    ApplicationName = appDomainName,
                                    ConfigurationFile = configFileName ?? string.Format("{0}.config", fileName),
                                    // TODO: Perhaps we should setup an even handler to reload the AppDomain similar to ASP.NET in IIS.
                                    ShadowCopyFiles = "true",
                                };
            var appDomain = AppDomain.CreateDomain(string.Format("AppDomainPoshRunner-{0}", appDomainName), null, setupInfo);
            try {
                var runner = appDomain.CreateInstanceFromAndUnwrap(assembly.Location, typeof(AppDomainPoshRunner).FullName);
                return ((AppDomainPoshRunner)runner).RunScript(fileName);
            }
            finally
            {
                AppDomain.Unload(appDomain);
            }
        }
        
        /// <summary>
        /// Run a PowerShell script in the <see cref="PSHost"/>
        /// </summary>
        /// <param name="fileName">The path to the script to run.</param>
        /// <param name="host">The <see cref="PSHost"/> to pass along to it.</param>
        /// <returns>Script output as an array of strings.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="host"/> is null.</exception>
        public static string[] RunScriptInPSHost(string fileName, PSHost host)
        {
            if (host == null)
            {
                throw new ArgumentNullException("host", "You must pass a PSHost.");
            }
            var runner = new AppDomainPoshRunner();
            return runner.RunScript(fileName, host);
        }



        /// <summary>
        /// Creates a PowerShell <see cref="Runspace"/> to run a script under.
        /// </summary>
        /// <param name="fileName">The name of the text file to run as a powershell script.</param>
        /// <param name="host">An optional existing PSHost to attach the namespace to.</param>
        /// <exception cref="FileNotFoundException">Thrown if the <paramref name="fileName"/> does not exist.</exception>
        /// <returns><see cref="PSHost"/></returns>
        private string[] RunScript(string fileName, PSHost host = null)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("PowerShell script not found", fileName);
            }

            var scriptText = File.ReadAllText(fileName);
            Collection<PSObject> results;
            using (var runspace = (host == null) ? RunspaceFactory.CreateRunspace() : RunspaceFactory.CreateRunspace(host))
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
