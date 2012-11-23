namespace JustAProgrammer.ADPR
{
    public interface IADPRConfig
    {
        string Script { get; }
        string ConfigFile { get; }
        string Log4NetConfigFile { get; }
        string AppDomainName { get; }
        bool ShadowCopyFiles { get; }
    }
}