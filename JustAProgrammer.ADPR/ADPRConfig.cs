using System;
using System.IO;

namespace JustAProgrammer.ADPR
{
    /// <summary>
    /// Configuration for an ADPR Appdomain.
    /// </summary>
    [Serializable]
    public sealed class ADPRConfig
    {
        private string _configFile;
        public string Script { get; set; }
        
        public string ConfigFile
        {
            get { return _configFile ?? string.Format("{0}.config", Path.GetFullPath(Script)); }
            set { _configFile = value; }
        }

        public string Log4NetConfigFile { get; set; }

        public string AppDomainName { get; set; }

        public bool ShadowCopyFiles { get; set; }

        public ADPRConfig()
        {
            AppDomainName = "AppDomainPoshRunner";
            Log4NetConfigFile = "ADPR.log4net.config";
        }

        public ADPRConfig(string fileName) : this()
        {
            Script = fileName;
        }
    }
}