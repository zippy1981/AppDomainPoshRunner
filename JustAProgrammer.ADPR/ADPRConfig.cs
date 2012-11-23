using System;

namespace JustAProgrammer.ADPR
{
    [Serializable]
    public  class ADPRConfig : IADPRConfig
    {
        /// <remarks>Despite what ReSharper says, this explicit constructor is necessary for serialization.</remarks>
        public ADPRConfig() { }

        public virtual string Script { get; set; }

        public string ConfigFile { get; set; }

        public string Log4NetConfigFile { get; set; }

        public string AppDomainName { get; set; }

        public bool ShadowCopyFiles { get; set; }

        public bool Help { get; set; }

    }
}