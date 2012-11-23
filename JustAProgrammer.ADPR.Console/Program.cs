using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

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
                Log4NetConfigFile = Path.Combine(Directory.GetParent(Assembly.GetEntryAssembly().Location).FullName, "ADPR.log4net.config");
            }


            [Parameters(Exact = 1)]
            public List<string> Parameters = new List<string>(1);

            public string Script {
                get
                {
                    return Parameters == null ? null : Parameters[0];
                } 
            }

            private string _configFile;
            [Argument("Config", Helptext = "The name of the app.config file for the script. Default is scriptName.config", Helpvar = "configFile")]
            public string ConfigFile
            {
                get { return _configFile ?? string.Format("{0}.config", Path.GetFullPath(Script)); }
                set { _configFile = value; }
            }
            
            [Argument("Log4NetConfig", Helptext = "Override the default config file for log4net.", Helpvar = "log4NetConfigFile")]
            public string Log4NetConfigFile { get; set; }

            [Argument("AppDomainName", Helptext = "Name to give the AppDomain the PowerShell script executes in.", Helpvar = "Name")]
            public string AppDomainName { get; set; }
            
            [FlagArgument(true)]
            [Argument("ShadowCopy", Helptext = "Enable Assembly ShadowCopying.")]
            public bool ShadowCopyFiles { get; set; }

            [FlagArgument(true)]
            [Argument("Help", Helptext = "Show help")]
            [ShortArgument('h')]
            [ShortArgumentAlias('?')]
            public bool Help { get; set; }

            /// <remarks>This is needed because <see cref="Opts"/> can't be serialized sine its inherits from <see cref="GetOpt"/></remarks>
            /// <returns>The class converted to an ADPRConfig file.</returns>
            public ADPRConfig ToADPRConfig()
            {
                return new ADPRConfig
                           {
                               Script = Script,
                               AppDomainName = AppDomainName,
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
                    //TODO: USage should go to the log4net appenders in this case
                    opts.PrintUsage();
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
    }
}
