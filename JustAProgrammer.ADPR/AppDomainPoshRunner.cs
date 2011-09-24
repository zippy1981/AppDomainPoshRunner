using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace JustAProgrammer.ADPR
{
    public class AppDomainPoshRunner : MarshalByRefObject
    {
        /// <summary>
        /// Creates a PowerShell runspace to run the script under.
        /// </summary>
        /// <param name="fileName">The name of the text file to run as a powershell script.</param>
        /// <exception cref="FileNotFoundException">Thrown if the <paramref name="fileName"/> does not exist.</exception>
        /// <returns></returns>
        public string[] RunScript(string fileName)
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
                    results = pipeline.Invoke();
                    runspace.Close();
                }
            }


            return (from result in results select result.ToString()).ToArray();
        }
    }
}
