using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Reflection;

namespace JustAProgrammer.ADPR
{
    public sealed class AppDomainPoshRunner : MarshalByRefObject
    {
        /// <summary>
        /// Created a new <see cref="AppDomain"/> and runs the given PowerShell script.
        /// </summary>
        /// <param name="fileName">The name of the PowerShell script to run.</param>
        /// <param name="configFileName">The name of the configuration file. If you set it to null it will default to <code><paramref name="fileName"/>.config</code>.</param>
        /// <param name="appDomainName">The name of the AppDomain.</param>
        /// <returns>The output of the script as an array of strings.</returns>
        public static string[] RunScriptInNewAppDomain(string fileName, string configFileName = null, string appDomainName = "AppDomainPoshRunner")
        {
            var assembly = Assembly.GetExecutingAssembly();
            
            var setupInfo = new AppDomainSetup
                                {
                                    ApplicationName = appDomainName,
                                    ConfigurationFile = configFileName ?? string.Format("{0}.config", Path.GetFullPath(fileName)),
                                    // TODO: Perhaps we should setup an even handler to reload the AppDomain similar to ASP.NET in IIS.
                                    ShadowCopyFiles = "true",
                                };
            var appDomain = AppDomain.CreateDomain(string.Format("AppDomainPoshRunner-{0}", appDomainName), null, setupInfo);
            try {
                var runner = appDomain.CreateInstanceFromAndUnwrap(assembly.Location, typeof(AppDomainPoshRunner).FullName);
                return ((AppDomainPoshRunner)runner).RunScript(new Uri(Path.GetFullPath(fileName)));
            }
            finally
            {
                AppDomain.Unload(appDomain);
            }
        }

        /// <summary>
        /// Creates a PowerShell <see cref="Runspace"/> to run a script under.
        /// </summary>
        /// <param name="file">The name of the text file to run as a powershell script.</param>
        /// <param name="host">An optional existing PSHost to attach the namespace to.</param>
        /// <exception cref="FileNotFoundException">Thrown if the <paramref name="file"/> does not exist.</exception>
        /// <returns></returns>
        public string[] RunScript(Uri file, ADPRHost host = null)
        {
            if (!File.Exists(file.LocalPath))
            {
                throw new FileNotFoundException("PowerShell script not found", file.AbsolutePath);
            }

            var script = File.ReadAllText(file.LocalPath);
            return RunScript(script, host);
        }

        /// <summary>
        /// Creates a PowerShell <see cref="Runspace"/> to run a script under.
        /// </summary>
        /// <param name="script">The script to run.</param>
        /// <param name="host">An optional existing PSHost to attach the namespace to.</param>
        /// <returns></returns>
        public string[] RunScript(string script, ADPRHost host = null)
        {
            Collection<PSObject> results;
            if (host == null)
            {
                var state = new ADPRState();
                host = new ADPRHost(state);
            }
            using (var runspace = (RunspaceFactory.CreateRunspace(host)))
            {
                runspace.Open();
                results = RunScript(runspace, script, true);
                runspace.Close();
            }

            return (from result in results select result.ToString()).ToArray();
        }

        /// <summary>
        /// Runs a PowerShell script under a given <see cref="Runspace"/>.
        /// </summary>
        /// <param name="runspace">The Runspace to run the script under.</param>
        /// <param name="scriptText">The name of the text file to run as a powershell script.</param>
        /// <param name="outString">Set to true to add an Out-String to the end of the pipeline.</param>
        /// <returns></returns>
        private static Collection<PSObject> RunScript(Runspace runspace, string scriptText, bool outString = false)
        {
            Collection<PSObject> results;
            using (var pipeline = runspace.CreatePipeline())
            {
                pipeline.Commands.AddScript(scriptText);
                if (outString)
                {
                    pipeline.Commands[0].MergeMyResults(PipelineResultTypes.Error, PipelineResultTypes.Output);;
                    //pipeline.Commands.Add("Out-String");
                    pipeline.Commands.Add("Out-Default");
                }
                results = pipeline.Invoke();
            }
            return results;
        }
    }
}
