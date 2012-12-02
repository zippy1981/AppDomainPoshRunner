using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using NMaier.GetOptNet;

namespace JustAProgrammer.ADPR.Console
{
    class Program
    {
        [Serializable]
        [GetOptOptions(OnUnknownArgument = UnknownArgumentsAction.Throw, CaseType = ArgumentCaseType.Insensitive)]
        private sealed class Opts : GetOpt, IADPRConfig
        {

            public Opts()
            {
                AppDomainName = "AppDomainPoshRunner";
                Log4NetConfigFile = null;
                Log4NetConfigType = Log4NetConfigType.Console;
            }

            [Argument("Script", Helptext = "Name of the script to run. ", Helpvar = "script")] 
            [ArgumentAlias("File")]
            [ShortArgument('f')]
            public string Script { get; set; }

            private string _configFile;
            [Argument("Config", Helptext = "The name of the app.config file for the script. Default is scriptName.config", Helpvar = "configFile")]
            public string ConfigFile
            {
                get { return _configFile ?? string.Format("{0}.config", Path.GetFullPath(Script)); }
                set { _configFile = value; }
            }

            [Argument("Log4NetConfigType", Helptext = "The type of Log4Net configuration.")]
            public Log4NetConfigType Log4NetConfigType { get; set; }
            
            [Argument("Log4NetConfig", Helptext = "Override the default config file for log4net.", Helpvar = "log4NetConfigFile")]
            public string Log4NetConfigFile { get; set; }

            [Argument("AppDomainName", Helptext = "Name to give the AppDomain the PowerShell script executes in.", Helpvar = "Name")]
            public string AppDomainName { get; set; }
            
            [FlagArgument(true)]
            [Argument("ShadowCopy", Helptext = "Enable Assembly ShadowCopying.")]
            public bool ShadowCopyFiles { get; set; }

            [FlagArgument(true)]
            [Argument("Help", Helptext = "Show help and exit")]
            [ShortArgument('h')]
            [ShortArgumentAlias('?')]
            public bool Help { get; set; }

            [FlagArgument(true)]
            [Argument("Version", Helptext = "Show version info and exit")]
            [ShortArgument('v')]
            public bool Version { get; set; }

            /// <remarks>This is needed because <see cref="Opts"/> can't be serialized sine its inherits from <see cref="GetOpt"/></remarks>
            /// <returns>The class converted to an ADPRConfig file.</returns>
            public ADPRConfig ToADPRConfig()
            {
                return new ADPRConfig
                           {
                               Script = Script,
                               AppDomainName = AppDomainName,
                               Log4NetConfigType = Log4NetConfigType,
                               Log4NetConfigFile = Log4NetConfigFile,
                               ConfigFile = ConfigFile,
                               Help = Help,
                               ShadowCopyFiles = ShadowCopyFiles
                           };
            }
        }

        static int Main(string[] args)
        {
            var opts = new Opts();
            try
            {
                opts.Parse(args);
                if (opts.Help)
                {
                    //TODO: Usage should go to the log4net appenders in this case
                    opts.PrintUsage();
                    return 0;
                }
                if (opts.Version)
                {
                    //TODO: Usage should go to the log4net appenders in this case
                    System.Console.WriteLine(GetVersionString());
                    return 0;
                }
                if (string.IsNullOrWhiteSpace(opts.Script))
                {
                    throw new GetOptException("Most specify a script!");
                }
                if (!File.Exists(opts.Script))
                {
                    //TODO: GetOptException needs a constuctor that takes an inner exception.
                    throw new GetOptException(string.Format("Script {0} not found.", opts.Script));
                }
                if (!string.IsNullOrWhiteSpace(opts.Log4NetConfigFile))
                {
                    if (!File.Exists(opts.Log4NetConfigFile))
                    {
                        //TODO: GetOptException needs a constuctor that takes an inner exception.
                        throw new GetOptException(string.Format("Log4net config file {0} not found.", opts.Log4NetConfigFile));
                    }
                    opts.Log4NetConfigType = Log4NetConfigType.Custom;
                }
            }
            catch (GetOptException ex)
            {
                System.Console.WriteLine("--> {0}", ex.Message);
                System.Console.WriteLine();
                opts.PrintUsage();
                return 2;
            }

            var results =  AppDomainPoshRunner.RunScriptInNewAppDomain(opts.ToADPRConfig());
            foreach (var result in results)
            {
                System.Console.WriteLine(result);
            }
            // TODO: Get the state from the PSHost
            // TODO if parent is console.exe present the press any key to continue prompt.

            return 0;
        }

        /// <summary>
        /// Generates the text displayed by <c>-v</c> and <c>--version</c>
        /// </summary>
        /// <returns></returns>
        private static string GetVersionString()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            var ret = new StringBuilder();
            ret.AppendFormat("{0} Version {1}", Path.GetFileName(assembly.Location), assembly.GetName().Version);
            ret.AppendLine();
            ret.AppendLine(fvi.LegalCopyright);
            return ret.ToString();
        }
    }
}
