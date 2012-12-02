
using log4net.Appender;

namespace JustAProgrammer.ADPR
{
    public interface IADPRConfig
    {
        string Script { get; }
        string ConfigFile { get; }
        Log4NetConfigType Log4NetConfigType { get; }
        string Log4NetConfigFile { get; }
        string AppDomainName { get; }
        bool ShadowCopyFiles { get; }
    }
}