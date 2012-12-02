namespace JustAProgrammer.ADPR
{
    /// <summary>Represents the type of Log4Net configurations</summary>
    public enum Log4NetConfigType : int
    {
        /// <summary>Use a custom file. You can set the name of this file with <see cref="IADPRConfig.Log4NetConfigFile"/></summary>
        CustomFile = 0,
        /// <summary>Use the Log4Net <see cref="log4net.Appender.ColoredConsoleAppender"/>.</summary>
        /// <remarks>This configuration is stored in the embedded configuration <see cref="JustAProgrammer.ADPR.ColoredConsoleAppender.log4net.config"/></remarks>
        ColoredConsoleAppender = 1,
        /// <summary>Use the Log4Net <see cref="log4net.Appender.RollingFileAppender"/>.</summary>
        /// <remarks>This configuration is stored in the embedded configuration <see cref="JustAProgrammer.ADPR.RollingAppender.log4net.config"/></remarks>
        RollingFileAppender = 2
    }
}